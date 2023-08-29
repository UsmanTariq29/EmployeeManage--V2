using EmployeeManage.Utilities.FileUpload;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;
using System;

namespace EmployeeManage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();

            var host = CreateHostBuilder(args).Build();

            try
            {

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var configuration = services.GetRequiredService<IConfiguration>();
                   
                    var tablename = configuration.GetValue<string>("Serilog:WriteTo:0:Args:tableName");
                    var overRide = configuration.GetValue<string>("Serilog:MinimumLevel:Override:value");

                    try
                    {
                        //var columnOptions = new ColumnOptions
                        //{
                        //    AdditionalColumns = new Collection<SqlColumn>
                        //    {
                        //        new SqlColumn("UserName",SqlDbType.VarChar)
                        //    }
                        //}; 
                        Log.Logger = new LoggerConfiguration()
                            .Enrich.FromLogContext()
                            .WriteTo.MSSqlServer(configuration.GetConnectionString(configuration.GetValue<string>("ConnectionStringNameSuffix")),
                            sinkOptions: new SinkOptions { TableName = tablename }
                            , restrictedToMinimumLevel: LogEventLevel.Information)
                            .MinimumLevel.Override(overRide, LogEventLevel.Error)
                            .CreateLogger();

                        DirectoryPermissionHelper.VerifyUploadDirectories(services);

                    
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }

                try
                {
                    host.Run();
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
            finally
            {
                // Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    } 
}