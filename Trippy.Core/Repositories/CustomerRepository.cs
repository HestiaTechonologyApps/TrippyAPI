using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.InfraCore.Data;

namespace Trippy.CORE.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly AppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        public CustomerRepository(AppDbContext context, ICurrentUserService currentUser) : base(context)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<IQueryable<CustomerDTO>> GetQuerableCustomerList()
        {
            var q = (from cust in _context.Customers
                     join cmp in _context.Companies on cust.CompanyId equals cmp.CompanyId
                     where cust.CompanyId == int.Parse(_currentUser.CompanyId)
                     where cust.IsDeleted == false
                     select new CustomerDTO
                     {
                         CustomerId = cust.CustomerId,
                         CustomerName = cust.CustomerName,
                         IsActive = cust.IsActive,
                         CustomerPhone = cust.CustomerPhone,
                         CustomerEmail = cust.CustomerEmail,
                         ComapanyName = cmp.ComapanyName,
                         CompanyId = cust.CompanyId,
                         IsDeleted = cust.IsDeleted,
                         CustomerAddress = cust.CustomerAddress,
                         DOB = cust.DOB,
                         
                         Nationality = cust.Nationalilty,
                     }).AsQueryable();
            return q;
        }
    }
}
