using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Trippy.Domain.Entities;
using Trippy.Domain.Entities;


namespace Trippy.InfraCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Public Users

        public DbSet<Company> Companies { get; set; }

        public DbSet<CompanyBranch> CompanyBranches { get; set; }

        public DbSet<Category> Categorys { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CategoryTax> CategoryTaxes { get; set; }
        public DbSet<FinancialYear> FinancialYears { get; set; }
        public DbSet<TripOrder> TripOrders { get; set; }
        public DbSet<TripNotes> TripNotes { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<ExpenseMaster> ExpenseMasters { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleMaintenanceRecord> VehicleMaintenanceRecords { get; set; }
        public DbSet<TripKiloMeter> TripKiloMeters { get; set; }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<TripBookingMode> TripBookingModes { get; set; }



        public DbSet<AuditLog> AuditLogs { get; set; }
       
        public DbSet<InvoiceMaster> InvoiceMasters { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<InvoiceDetailTax> InvoiceDetailTaxes { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
    
     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         



        }
    }
}
