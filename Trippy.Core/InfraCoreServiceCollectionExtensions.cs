using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.Domain.Interfaces.IServices;
using Trippy.InfraCore.Data;
using Trippy.InfraCore.External;
using Trippy.InfraCore.Repositories;
using Trippy.CORE.Repositories;
using Trippy.Core.Repositories;


namespace Trippy.InfraCore
{
    public static class InfraCoreServiceCollectionExtensions
    {
             public static IServiceCollection AddInfraCoreServiceCollectionExtensions(this IServiceCollection services, IConfiguration configuration)
        {
       
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Register application-level services here
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            
            services.AddScoped<IAuditLogRepository, AuditLogRepository>();
            
            services.AddScoped<IExceptionLogRepository , ExceptionLogRepository>();
            services.AddScoped<IAuditRepository , AuditRepository>();
          
            services.AddScoped<IFinancialYearRepository  , FinancialYearRepository>();
            services.AddScoped<IDriverRepository  , DriverRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
           
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyBranchRepository, CompanyBranchRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryTaxRepository, CategoryTaxRepository>();
            services.AddScoped<ITwilioSmsSender, TwilioSmsSender>();
            services.AddScoped<ICustomResponseBuilder, CustomResponseBuilder>();

            //// Add helpers or utilities
            //services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            return services;
        }
    }
}
