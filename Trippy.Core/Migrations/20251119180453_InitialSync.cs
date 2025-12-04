using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trippy.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialSync : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Attachments",
            //    columns: table => new
            //    {
            //        AttachmentId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        AttachmentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        AttachmentPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        FileSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        FileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        RecordID = table.Column<int>(type: "int", nullable: true),
            //        UploaddedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        UploadedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false),
            //        DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Attachments", x => x.AttachmentId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AuditLogs",
            //    columns: table => new
            //    {
            //        LogID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        RecordID = table.Column<int>(type: "int", nullable: true),
            //        ChangedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        ChangeDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AuditLogs", x => x.LogID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Categorys",
            //    columns: table => new
            //    {
            //        CategoryId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CategoryTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CategoryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CompanyId = table.Column<int>(type: "int", nullable: false),
            //        CategoryImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Categorys", x => x.CategoryId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "CategoryTaxes",
            //    columns: table => new
            //    {
            //        CategoryTaxId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CategoryTaxName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CategoryTaxCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CategoryTaxPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_CategoryTaxes", x => x.CategoryTaxId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Companies",
            //    columns: table => new
            //    {
            //        CompanyId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ComapanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        City = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        State = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        InvoicePrefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CompanyLogo = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Companies", x => x.CompanyId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "CompanyBranches",
            //    columns: table => new
            //    {
            //        CompanyBranchId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CompanyId = table.Column<int>(type: "int", nullable: false),
            //        BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        City = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        State = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_CompanyBranches", x => x.CompanyBranchId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Customers",
            //    columns: table => new
            //    {
            //        CustomerId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CustomerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Nationalilty = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Customers", x => x.CustomerId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Drivers",
            //    columns: table => new
            //    {
            //        DriverId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        DriverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        License = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ProfileImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        NationalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        IsRented = table.Column<bool>(type: "bit", nullable: false),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Drivers", x => x.DriverId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ExceptionLogs",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        LoggedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        QueryString = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        User = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Headers = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ExceptionMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ExceptionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        InnerException = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        TraceId = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ExceptionLogs", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ExpenseMasters",
            //    columns: table => new
            //    {
            //        ExpenseMasterId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ExpenseTypeId = table.Column<int>(type: "int", nullable: false),
            //        Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        ExpenseVoucher = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Remark = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        RelatedEntityId = table.Column<int>(type: "int", nullable: false),
            //        RelatedEntityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ExpenseMasters", x => x.ExpenseMasterId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ExpenseTypes",
            //    columns: table => new
            //    {
            //        ExpenseTypeId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ExpenseTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ExpenseTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CreditDebitType = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ExpenseTypes", x => x.ExpenseTypeId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "FinancialYears",
            //    columns: table => new
            //    {
            //        FinancialYearId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        FinacialYearCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        IsCurrent = table.Column<bool>(type: "bit", nullable: false),
            //        IsClosed = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_FinancialYears", x => x.FinancialYearId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "InvoiceDetails",
            //    columns: table => new
            //    {
            //        InvoiceDetailId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        InvoiceMasterId = table.Column<int>(type: "int", nullable: false),
            //        TripOrderId = table.Column<int>(type: "int", nullable: false),
            //        CategoryId = table.Column<int>(type: "int", nullable: false),
            //        Ammount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        TotalTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_InvoiceDetails", x => x.InvoiceDetailId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "InvoiceDetailTaxes",
            //    columns: table => new
            //    {
            //        InvoiceDetailTaxId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        InvoiceDetailId = table.Column<int>(type: "int", nullable: false),
            //        CategoryTaxId = table.Column<int>(type: "int", nullable: false),
            //        CategoryTaxPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_InvoiceDetailTaxes", x => x.InvoiceDetailTaxId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "InvoiceMasters",
            //    columns: table => new
            //    {
            //        InvoiceMasterId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        InvoiceNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        FinancialYearId = table.Column<int>(type: "int", nullable: false),
            //        CompanyId = table.Column<int>(type: "int", nullable: false),
            //        TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_InvoiceMasters", x => x.InvoiceMasterId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TripBookingModes",
            //    columns: table => new
            //    {
            //        TripBookingModeId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        TripBookingModeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TripBookingModes", x => x.TripBookingModeId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TripKiloMeters",
            //    columns: table => new
            //    {
            //        TripKiloMeterId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        TripOrderId = table.Column<int>(type: "int", nullable: false),
            //        DriverId = table.Column<int>(type: "int", nullable: false),
            //        VehicleId = table.Column<int>(type: "int", nullable: false),
            //        TripStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        TripEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        TripStartReading = table.Column<int>(type: "int", nullable: false),
            //        TripEndReading = table.Column<int>(type: "int", nullable: false),
            //        CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TripKiloMeters", x => x.TripKiloMeterId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TripNotes",
            //    columns: table => new
            //    {
            //        TripNoteId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        TripNote = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TripNotes", x => x.TripNoteId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TripOrders",
            //    columns: table => new
            //    {
            //        TripOrderId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        TripBookingModeId = table.Column<int>(type: "int", nullable: false),
            //        TripCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CustomerId = table.Column<int>(type: "int", nullable: false),
            //        DriverId = table.Column<int>(type: "int", nullable: false),
            //        FromDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        FromDateString = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ToDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        ToDateString = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        FromLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ToLocation1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ToLocation2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ToLocation3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ToLocation4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PaymentDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        BookedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        TripDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        TripStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        TripAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        AdvanceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        BalanceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TripOrders", x => x.TripOrderId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Users",
            //    columns: table => new
            //    {
            //        UserId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false),
            //        Islocked = table.Column<bool>(type: "bit", nullable: false),
            //        CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Lastlogin = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Users", x => x.UserId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "VehicleMaintenanceRecords",
            //    columns: table => new
            //    {
            //        VehicleMaintenanceRecordId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        VehicleId = table.Column<int>(type: "int", nullable: false),
            //        MaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        MaintenanceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        WorkshopName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        OdometerReading = table.Column<int>(type: "int", nullable: false),
            //        PerformedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_VehicleMaintenanceRecords", x => x.VehicleMaintenanceRecordId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Vehicles",
            //    columns: table => new
            //    {
            //        VehicleId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Make = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Year = table.Column<int>(type: "int", nullable: false),
            //        ChassisNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        EngineNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        VehicleType = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        RegistrationExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        CurrentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
            //    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Attachments");

            //migrationBuilder.DropTable(
            //    name: "AuditLogs");

            //migrationBuilder.DropTable(
            //    name: "Categorys");

            //migrationBuilder.DropTable(
            //    name: "CategoryTaxes");

            //migrationBuilder.DropTable(
            //    name: "Companies");

            //migrationBuilder.DropTable(
            //    name: "CompanyBranches");

            //migrationBuilder.DropTable(
            //    name: "Customers");

            //migrationBuilder.DropTable(
            //    name: "Drivers");

            //migrationBuilder.DropTable(
            //    name: "ExceptionLogs");

            //migrationBuilder.DropTable(
            //    name: "ExpenseMasters");

            //migrationBuilder.DropTable(
            //    name: "ExpenseTypes");

            //migrationBuilder.DropTable(
            //    name: "FinancialYears");

            //migrationBuilder.DropTable(
            //    name: "InvoiceDetails");

            //migrationBuilder.DropTable(
            //    name: "InvoiceDetailTaxes");

            //migrationBuilder.DropTable(
            //    name: "InvoiceMasters");

            //migrationBuilder.DropTable(
            //    name: "TripBookingModes");

            //migrationBuilder.DropTable(
            //    name: "TripKiloMeters");

            //migrationBuilder.DropTable(
            //    name: "TripNotes");

            //migrationBuilder.DropTable(
            //    name: "TripOrders");

            //migrationBuilder.DropTable(
            //    name: "Users");

            //migrationBuilder.DropTable(
            //    name: "VehicleMaintenanceRecords");

            //migrationBuilder.DropTable(
            //    name: "Vehicles");
        }
    }
}
