using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Bussiness.Services;
using Trippy.Domain.Configurations;
using Trippy.Domain.Interfaces.IServices;




namespace Trippy.Bussiness
{
   public static class BussinessServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IJwtService, JwtService>();            
           
            services.AddScoped<IAuditLogService, AuditLogService>();
           
           
           
            services.AddScoped<IExceptionLogService, ExceptionLogService>();
          
            
            services.AddScoped<IFinancialYearService, FinancialYearService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICompanyBranchService, CompanyBranchService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryTaxService, CategoryTaxService>();
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IUserService, UserService>();
            //services.Configure<OtpSettings>(configuration.GetSection("OtpSettings"));
            //services.Configure<WalletSettings>(configuration.GetSection("Wallet"));

            // Add helpers or utilities
            //services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            return services;
        }
    }
}
