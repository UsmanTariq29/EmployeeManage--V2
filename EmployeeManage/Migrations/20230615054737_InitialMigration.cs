using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManage.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserGuid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchGuid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyGuid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblAcccountMaincode",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainCodeID = table.Column<int>(type: "int", nullable: false),
                    MainCodeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MainCodeNameArabic = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MainCodeNameFrench = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AccountTypeID = table.Column<int>(type: "int", nullable: false),
                    AccountTypeGroupID = table.Column<int>(type: "int", nullable: true),
                    IsSystemAccount = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedByUserGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsSuspended = table.Column<bool>(type: "bit", nullable: false),
                    CompanyGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblAcccountMaincode", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TblAccountDetailcode",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainCodeID = table.Column<int>(type: "int", nullable: false),
                    DetailCodeID = table.Column<int>(type: "int", nullable: false),
                    DetailCodeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DetailCodeNameArabic = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DetailCodeNameFrench = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsSystemAccount = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedByUserGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsSuspended = table.Column<bool>(type: "bit", nullable: false),
                    CompanyGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblAccountDetailcode", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TblAccountSubcode",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCodeID = table.Column<int>(type: "int", nullable: false),
                    DetailCodeID = table.Column<int>(type: "int", nullable: false),
                    SubCodeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SubCodeNameFrench = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SubCodeNameArabic = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CurrencyID = table.Column<int>(type: "int", nullable: true),
                    PhysicalAccountName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PhysicalAccountNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PhysicalAccountTypeID = table.Column<int>(type: "int", nullable: true),
                    PhysicalAccountSwiftCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhysicalAccountIBANNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CryptocurrencyPublicKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WalletNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsSystemAccount = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedByUserGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsSuspended = table.Column<bool>(type: "bit", nullable: false),
                    CompanyGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblAccountSubcode", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TblAccountType",
                columns: table => new
                {
                    AccountTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AccountNameArabic = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AccountNameFrench = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AccountDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AccountDescriptionArabic = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AccountDescriptionFrench = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ACCOUNT_TYPE", x => x.AccountTypeID);
                });

            migrationBuilder.CreateTable(
                name: "TblAccountTypeGroup",
                columns: table => new
                {
                    AccountTypeGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountTypeID = table.Column<int>(type: "int", nullable: false),
                    AccountTypeGroup = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountTypeGroupFrench = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AccountTypeGroupArabic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ACCOUNT_TYPE_GROUP", x => x.AccountTypeGroupID);
                });

            migrationBuilder.CreateTable(
                name: "TblBranch",
                columns: table => new
                {
                    BranchID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchGuid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BranchShortName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CompanyGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblBranches", x => x.BranchID);
                });

            migrationBuilder.CreateTable(
                name: "TblCity",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CityName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCity", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "TblCompany",
                columns: table => new
                {
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyGuid = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CompanyName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CompanyAddress = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCompany", x => x.CompanyID);
                });

            migrationBuilder.CreateTable(
                name: "TblCountry",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    CountryName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCountry", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "TblCustomer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    NTNNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CNICNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CreatedByUserGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BranchGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCustomer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "TblDepartment",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblDepartment", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "TblDocumentCategory",
                columns: table => new
                {
                    DocumentCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblDocumentCategory", x => x.DocumentCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "TblEmployee",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    EmployeeEmail = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    GrossSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    NationalityID = table.Column<int>(type: "int", nullable: false),
                    BranchID = table.Column<int>(type: "int", nullable: false),
                    PhotoPath = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: true),
                    JoinDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblEmployee", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "TblEmployeeAttandance",
                columns: table => new
                {
                    AttandanceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    AttandanceDate = table.Column<DateTime>(type: "date", nullable: false),
                    TimeIN = table.Column<TimeSpan>(type: "time", nullable: false),
                    TimeOut = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblEmployeeAttandance", x => x.AttandanceID);
                });

            migrationBuilder.CreateTable(
                name: "TblEmployeeDocument",
                columns: table => new
                {
                    EmployeeDocumentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    DocumentCategoryID = table.Column<int>(type: "int", nullable: false),
                    DocumentPath = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "date", nullable: false),
                    DocumentWithExpiry = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblEmployeesDocument", x => x.EmployeeDocumentID);
                });

            migrationBuilder.CreateTable(
                name: "TblEmployeeManagementLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblEmployeeManagementLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblHold_InvoiceMaster",
                columns: table => new
                {
                    HoldOrderMasterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CreatedByUserGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BranchGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblHold_InvoiceMaster", x => x.HoldOrderMasterId);
                });

            migrationBuilder.CreateTable(
                name: "TblHoldOrderDetail",
                columns: table => new
                {
                    HoldOrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoldOrderMasterId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblHold_invoiceDetail", x => x.HoldOrderDetailId);
                });

            migrationBuilder.CreateTable(
                name: "TblHoldOrderMaster",
                columns: table => new
                {
                    HoldInvoiceMasterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CreatedByUserGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BranchGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblHoldOrderMaster", x => x.HoldInvoiceMasterId);
                });

            migrationBuilder.CreateTable(
                name: "TblInvoiceDetail",
                columns: table => new
                {
                    InvoiceDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceMasterId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblInvoiceDetail", x => x.InvoiceDetailId);
                });

            migrationBuilder.CreateTable(
                name: "TblInvoiceMaster",
                columns: table => new
                {
                    InvoiceMasterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    AmountReceived = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CreatedByUserGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BranchGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice_Master_Detail", x => x.InvoiceMasterId);
                });

            migrationBuilder.CreateTable(
                name: "TblItemBarcode",
                columns: table => new
                {
                    BarcodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    ItemBarcode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblBarcode", x => x.BarcodeId);
                });

            migrationBuilder.CreateTable(
                name: "TblItemGroup",
                columns: table => new
                {
                    ItemGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemGroupName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ItemCreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserGuId = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CompanyGuid = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblItem_Group", x => x.ItemGroupId);
                });

            migrationBuilder.CreateTable(
                name: "TblItemPrice",
                columns: table => new
                {
                    PriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    CompanyGUID = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    ItemsPrice = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    UserGUID = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblItemPrice", x => x.PriceId);
                });

            migrationBuilder.CreateTable(
                name: "TblItems",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ItemName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    IsExempted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ItemDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserGuId = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CompanyGuid = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ItemReOrder = table.Column<int>(type: "int", nullable: false),
                    ItemGroupId = table.Column<int>(type: "int", nullable: false),
                    IsBatchItem = table.Column<bool>(type: "bit", nullable: false),
                    IsRepalaceable = table.Column<bool>(type: "bit", nullable: false),
                    IsExpiryAllowed = table.Column<bool>(type: "bit", nullable: false),
                    UnitInCase = table.Column<int>(type: "int", nullable: false),
                    TaxId = table.Column<int>(type: "int", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,4)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblItems", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "TblItemsPriceLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblItemsPriceLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblPromotionDetail",
                columns: table => new
                {
                    PromotionDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromotionMasterId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    PromotionPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblPromotionDetail", x => x.PromotionDetailId);
                });

            migrationBuilder.CreateTable(
                name: "TblPromotionMaster",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromotionName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PromotionStartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    PromotionEndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    PromotionDescription = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    PromotionType = table.Column<int>(type: "int", nullable: false),
                    PromotionPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedByUserGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BranchGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblPromotionMaster", x => x.PromotionId);
                });

            migrationBuilder.CreateTable(
                name: "TblPromotionType",
                columns: table => new
                {
                    PromotionTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromotionType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblPromotionType", x => x.PromotionTypeId);
                });

            migrationBuilder.CreateTable(
                name: "TblPurchaseDetail",
                columns: table => new
                {
                    purchaseDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseMasterId = table.Column<int>(type: "int", nullable: false),
                    ItemGroupId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "char(100)", unicode: false, fixedLength: true, maxLength: 100, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FixedDiscount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    AmountAfterDiscount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ItemTaxId = table.Column<int>(type: "int", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblPurchaseDetail", x => x.purchaseDetailId);
                });

            migrationBuilder.CreateTable(
                name: "TblPurchaseMaster",
                columns: table => new
                {
                    PurchaseMasterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    InvoiceNo = table.Column<string>(type: "char(100)", unicode: false, fixedLength: true, maxLength: 100, nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    TotalDiscount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    TotalTax = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CreatedByUserGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BranchGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VoucherDisplayNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblPurchaseMaster", x => x.PurchaseMasterId);
                });

            migrationBuilder.CreateTable(
                name: "TblRegion",
                columns: table => new
                {
                    RegionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblRegion", x => x.RegionId);
                });

            migrationBuilder.CreateTable(
                name: "TblSupplier",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CNIC = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByUserGUID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    BranchGUID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CompanyGUID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DetailCodeID = table.Column<int>(type: "int", nullable: true),
                    SubCodeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblSupplier", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "TblTax",
                columns: table => new
                {
                    TaxId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    branchGUID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    companyGUID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedByUserGUID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblTax", x => x.TaxId);
                });

            migrationBuilder.CreateTable(
                name: "TblUnitOfMeasure",
                columns: table => new
                {
                    UnitOfMeasureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UnitOfMeasureDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUnitOfMeasure", x => x.UnitOfMeasureId);
                });

            migrationBuilder.CreateTable(
                name: "TblUser",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGuid = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Passwordexp = table.Column<DateTime>(type: "datetime", nullable: false),
                    Role = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CompanyGuid = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    BranchGuid = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUser", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TblVoucherDetail",
                columns: table => new
                {
                    VoucherDetailGUID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    VoucherMasterGUID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    SequenceNumber = table.Column<short>(type: "smallint", nullable: false),
                    DetailCodeID = table.Column<int>(type: "int", nullable: false),
                    SubCodeID = table.Column<int>(type: "int", nullable: false),
                    ForeignCurrencyAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    BaseCurrencyAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DetailCodeIDOne = table.Column<int>(type: "int", nullable: false),
                    SubCodeIDOne = table.Column<int>(type: "int", nullable: false),
                    CurrencyID = table.Column<int>(type: "int", nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    IsDebit = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedByTeller = table.Column<bool>(type: "bit", nullable: true),
                    IsCurrencyStockEntry = table.Column<bool>(type: "bit", nullable: true),
                    IsBalanceEntry = table.Column<bool>(type: "bit", nullable: true),
                    StockID = table.Column<int>(type: "int", nullable: true),
                    DescriptionReference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DetailCodeIDReference = table.Column<int>(type: "int", nullable: true),
                    SubcodeIDReference = table.Column<int>(type: "int", nullable: true),
                    TaxID = table.Column<int>(type: "int", nullable: true),
                    TaxPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    TaxType = table.Column<short>(type: "smallint", nullable: true, comment: "ADD = 1, EXCLUDE = 2"),
                    TotalAmountIncludingTax = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    AnalysisCodeTypeID = table.Column<int>(type: "int", nullable: true),
                    AnalysisCodeDetailCodeID = table.Column<int>(type: "int", nullable: true),
                    AnalysisCodeSubcodeID = table.Column<int>(type: "int", nullable: true),
                    AnalysisCodeNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Aux_1_big = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Aux_2_big = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CompanyGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_VOUCHER_DETAIL", x => x.VoucherDetailGUID);
                });

            migrationBuilder.CreateTable(
                name: "TblVoucherDisplayNumber",
                columns: table => new
                {
                    VoucherTypeID = table.Column<int>(type: "int", nullable: false),
                    BranchGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VoucherDisplayNumber = table.Column<int>(type: "int", nullable: false),
                    CompanyGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblVoucherDisplayNumber", x => new { x.VoucherTypeID, x.BranchGUID });
                });

            migrationBuilder.CreateTable(
                name: "TblVoucherMaster",
                columns: table => new
                {
                    VoucherGUID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    VoucherDisplayID = table.Column<int>(type: "int", nullable: false),
                    VoucherDisplayNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    VoucherCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserVoucherCreatedDateOnly = table.Column<DateTime>(type: "datetime", nullable: false),
                    VoucherTypeID = table.Column<int>(type: "int", nullable: false),
                    InvoiceNo = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    DetailCodeID = table.Column<int>(type: "int", nullable: true),
                    SubCodeID = table.Column<int>(type: "int", nullable: true),
                    MasterNarration = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ChequeDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SystemVoucherTypeId = table.Column<int>(type: "int", nullable: true),
                    TransactionReferenceDetailCodeID = table.Column<int>(type: "int", nullable: true),
                    TransactionReferenceSubcodeID = table.Column<int>(type: "int", nullable: true),
                    HideTransaction = table.Column<bool>(type: "bit", nullable: true),
                    ReferenceVoucherNumber = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: true),
                    ReferenceTransactionNumber = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: true),
                    BaseCurrencyID = table.Column<int>(type: "int", nullable: true),
                    AmountFC = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    AmountLC = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    ChequeNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PreviousEntryReference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DetailOne = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DetailTwo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DetailThree = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WithholdingTaxPercentage = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    NetAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    IsMultiCurrencyJV = table.Column<bool>(type: "bit", nullable: true),
                    ChangesDetail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    isPosted = table.Column<bool>(type: "bit", nullable: true),
                    BankDetailCodeID = table.Column<int>(type: "int", nullable: true),
                    BankSubCodeID = table.Column<int>(type: "int", nullable: true),
                    MasterReferenceNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CancelledByUserGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SupervisedByUserGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ClearedByUserGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CancelledDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SupervisionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClearedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    StatusIDTwo = table.Column<int>(type: "int", nullable: true),
                    CreatedByUserGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CashierAccountGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BranchGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_VOUCHER_MASTER", x => x.VoucherGUID);
                });

            migrationBuilder.CreateTable(
                name: "TblVoucherMasterss",
                columns: table => new
                {
                    VoucherMasterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNo = table.Column<int>(type: "int", nullable: false),
                    ReferenceVoucherNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VoucherTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BranchGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyGUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblVoucherMaster", x => x.VoucherMasterId);
                });

            migrationBuilder.CreateTable(
                name: "TblVoucherStatus",
                columns: table => new
                {
                    VoucherStatusID = table.Column<byte>(type: "tinyint", nullable: false),
                    VoucherStatusDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VoucherStatusDescriptionFrench = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VoucherStatusDescriptionArabic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_VOUCHER_STATUS", x => x.VoucherStatusID);
                });

            migrationBuilder.CreateTable(
                name: "TblVoucherType",
                columns: table => new
                {
                    VoucherTypeID = table.Column<int>(type: "int", nullable: false),
                    VoucherType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VoucherDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblVoucherType", x => x.VoucherTypeID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblEmployeeAllowance",
                columns: table => new
                {
                    AllowanceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    AllowanceDetail = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAllowances", x => x.AllowanceID);
                    table.ForeignKey(
                        name: "FK_TblEmployeeAllowances_TblEmployee",
                        column: x => x.EmployeeID,
                        principalTable: "TblEmployee",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MAIN_CODE_TBL_MAIN_ACCOUNT",
                table: "TblAcccountMaincode",
                columns: new[] { "CompanyGUID", "MainCodeID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ACCOUNT_NUMBER_TBL_SUBCODE_ACCOUNT",
                table: "TblAccountSubcode",
                columns: new[] { "CompanyGUID", "DetailCodeID", "SubCodeID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblEmployeeAllowance_EmployeeID",
                table: "TblEmployeeAllowance",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "UQ__TblItem___0141EE924FE72013",
                table: "TblItemGroup",
                column: "ItemGroupName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__TblUnitO__2F34111F3B588796",
                table: "TblUnitOfMeasure",
                column: "UnitOfMeasure",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "TblAcccountMaincode");

            migrationBuilder.DropTable(
                name: "TblAccountDetailcode");

            migrationBuilder.DropTable(
                name: "TblAccountSubcode");

            migrationBuilder.DropTable(
                name: "TblAccountType");

            migrationBuilder.DropTable(
                name: "TblAccountTypeGroup");

            migrationBuilder.DropTable(
                name: "TblBranch");

            migrationBuilder.DropTable(
                name: "TblCity");

            migrationBuilder.DropTable(
                name: "TblCompany");

            migrationBuilder.DropTable(
                name: "TblCountry");

            migrationBuilder.DropTable(
                name: "TblCustomer");

            migrationBuilder.DropTable(
                name: "TblDepartment");

            migrationBuilder.DropTable(
                name: "TblDocumentCategory");

            migrationBuilder.DropTable(
                name: "TblEmployeeAllowance");

            migrationBuilder.DropTable(
                name: "TblEmployeeAttandance");

            migrationBuilder.DropTable(
                name: "TblEmployeeDocument");

            migrationBuilder.DropTable(
                name: "TblEmployeeManagementLogs");

            migrationBuilder.DropTable(
                name: "TblHold_InvoiceMaster");

            migrationBuilder.DropTable(
                name: "TblHoldOrderDetail");

            migrationBuilder.DropTable(
                name: "TblHoldOrderMaster");

            migrationBuilder.DropTable(
                name: "TblInvoiceDetail");

            migrationBuilder.DropTable(
                name: "TblInvoiceMaster");

            migrationBuilder.DropTable(
                name: "TblItemBarcode");

            migrationBuilder.DropTable(
                name: "TblItemGroup");

            migrationBuilder.DropTable(
                name: "TblItemPrice");

            migrationBuilder.DropTable(
                name: "TblItems");

            migrationBuilder.DropTable(
                name: "TblItemsPriceLog");

            migrationBuilder.DropTable(
                name: "TblPromotionDetail");

            migrationBuilder.DropTable(
                name: "TblPromotionMaster");

            migrationBuilder.DropTable(
                name: "TblPromotionType");

            migrationBuilder.DropTable(
                name: "TblPurchaseDetail");

            migrationBuilder.DropTable(
                name: "TblPurchaseMaster");

            migrationBuilder.DropTable(
                name: "TblRegion");

            migrationBuilder.DropTable(
                name: "TblSupplier");

            migrationBuilder.DropTable(
                name: "TblTax");

            migrationBuilder.DropTable(
                name: "TblUnitOfMeasure");

            migrationBuilder.DropTable(
                name: "TblUser");

            migrationBuilder.DropTable(
                name: "TblVoucherDetail");

            migrationBuilder.DropTable(
                name: "TblVoucherDisplayNumber");

            migrationBuilder.DropTable(
                name: "TblVoucherMaster");

            migrationBuilder.DropTable(
                name: "TblVoucherMasterss");

            migrationBuilder.DropTable(
                name: "TblVoucherStatus");

            migrationBuilder.DropTable(
                name: "TblVoucherType");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TblEmployee");
        }
    }
}
