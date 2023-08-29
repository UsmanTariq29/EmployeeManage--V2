using EmployeeManage.Model;
using EmployeeManage.Repository;
using EmployeeManage.Repository.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.FileProviders;

using EmployeeManage.Utilities.FileUpload;
using EmployeeManage.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using EmployeeManage.ViewModels.Request;

namespace EmployeeManage
{
    public class Startup
    {
        private IConfiguration _confiq;

        public Startup(IConfiguration config)
        {
            _confiq = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
       public void ConfigureServices(IServiceCollection services)
        { 
            // register root upload path as file provider
            var fileProvider = new PhysicalFileProvider(_confiq.GetValue<string>("FileUpload:RootPath"));
            services.AddSingleton<IFileProvider>(fileProvider);
            // register file upload configurations as options
            foreach (FileUploadConfigTypeEnum config in Enum.GetValues(typeof(FileUploadConfigTypeEnum)))
            {
                services.Configure<FileUploadConfig>(config.ToString(),
                    _confiq.GetSection($"FileUpload:Config:{config}")
                );
            }
            services.AddIdentity<CustomRegisterUser, IdentityRole>()
            .AddEntityFrameworkStores<EmployeesDBContext>()
            .AddDefaultTokenProviders();

            //services.AddIdentity<IdentityUser, IdentityRole>()
            //.AddEntityFrameworkStores<EmployeesDBContext>()
            //.AddDefaultTokenProviders();


            // services.AddDbContextPool<EmployeesDBContext>(options => options.UseSqlServer(_confiq.GetConnectionString("ConnectionStringName")));
            // services.AddDbContextPool<SPToCoreContext>(options => options.UseSqlServer(_confiq.GetConnectionString("EmployeeDbConnection")));
            services.AddDbContext<EmployeesDBContext>(options => options.UseSqlServer(
               _confiq.GetConnectionString(_confiq.GetValue<string>("ConnectionStringNameSuffix"))));

            services.AddMvc(options =>options.EnableEndpointRouting = false).AddXmlDataContractSerializerFormatters() ;

            services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            services.AddTransient<IDepartmentRepo, DepartmentRepo>();
            services.AddTransient<IDocumentCount, DocumentCountRepo>();
            services.AddTransient<IRegionCountryCity, RegionCountryCityRepo>();
            services.AddTransient<IDocument, DocumentRepo>();
            services.AddScoped<IUser, UserRepo>();
            services.AddScoped<IBarcodeRepo, BarcodeRepo>();
            services.AddScoped<IDashboardRepo, DashboardRepo>();
            services.AddScoped<IPromotionRepo, PromotionRepo>();
            services.AddScoped<ISupplierRepo, SupplierRepo>();
            services.AddScoped<IPurchaseRepo, PurchaseRepo>();
            services.AddScoped<ITaxRepo, TaxRepo>();
            services.AddScoped<IItemRepo, ItemRepo>();
            services.AddScoped<IUnitOfMeasureRepo, UnitofMeasureRepo>();
            services.AddScoped<IItemGroupRepo, ItemGroupRepo>();
            services.AddScoped<IVoucherHelperRepo, VoucherHelperRepo>();
            services.AddScoped<SignInManager<CustomRegisterUser>>();

            services.AddDistributedMemoryCache();

            services.AddHttpContextAccessor();
            services.AddSession(options => 
            {
                options.IOTimeout = TimeSpan.FromMinutes(5);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseSession();
            app.UseStaticFiles();
            app.UseAuthentication();            

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Login}/{action=LoginUser}/{id?}");
              //  routes.MapRoute("default", "{controller=Login}/{action=RegisterUser}/{id?}");
//                routes.MapRoute("default", "{controller=Login}/{action=Index}/{id?}");
                //           routes.MapRoute("default", "{controller=Barcode}/{action=BarcodeSearch}/{id?}");
            });
        }
    }
}
