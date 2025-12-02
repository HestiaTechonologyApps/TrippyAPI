using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.InfraCore.Data;

namespace Trippy.Core.Repositories
{
    public class CustomerDepartmentRepository : GenericRepository<CustomerDepartment>, ICustomerDepartmentRepository
    {
        private readonly AppDbContext _context;
        public CustomerDepartmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        //public IQueryable<CustomerDepartment> GetAllCustomerDepartments()
        //{
           
        //}
    }
}
