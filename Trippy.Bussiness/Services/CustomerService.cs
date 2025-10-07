using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRIPPY.DOMAIN.Interfaces.IRepositories;
using Trippy.Domain.Interfaces.IServices;
using TRIPPY.DOMAIN.Interfaces.IServices;
using TRIPPY.DOMAIN.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.DTO;

namespace TRIPPY.BUSSINESS.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;
        public CustomerService(ICustomerRepository repo)
        {
            _repo = repo;
        }
        public async Task<CustomerDTO> CreateAsync(Customer customer)
        {
            await _repo.AddAsync(customer);
            await _repo.SaveChangesAsync();
            return ConvertCustomerToDTO(customer);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null) return false;
            _repo.Delete(customer);
            await _repo.SaveChangesAsync();
            return true;
        }
        public async Task<List<CustomerDTO>> GetAllAsync()
        {


            List<CustomerDTO> customerdtos = new List<CustomerDTO>();

            var customers = await _repo.GetAllAsync();

            foreach (var driver in customers)
            {
                CustomerDTO customerDTO = ConvertCustomerToDTO(driver);
                customerdtos.Add(customerDTO);


            }

            return customerdtos;
        }

        public async Task<CustomerDTO?> GetByIdAsync(int id)
        {
            var q = await _repo.GetByIdAsync(id);
            return q  == null ? null : ConvertCustomerToDTO(q);
        }

        public async Task<bool> UpdateAsync(Customer coupon)
        {
            _repo.Update(coupon);
            await _repo.SaveChangesAsync();
            return true;
        }

        private CustomerDTO ConvertCustomerToDTO(Customer customer)
        {
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.CustomerId = customer.CustomerId;
            customerDTO.CustomerName = customer.CustomerName;
            customerDTO.CustomerPhone = customer.CustomerPhone;
            customerDTO.CustomerEmail = customer.CustomerEmail;
            customerDTO.CustomerAddress = customer.CustomerAddress;
            customerDTO.DOB = customer.DOB;
            customerDTO.DOBString= customer.DOB.ToString("dd MMMM yyyy hh:mm tt");
            customerDTO.Gender = customer.Gender;
            customerDTO.Nationality = customer.Nationalilty;
            customerDTO.IsActive = customer.IsActive;
            customerDTO.CreatedAt = customer.CreatedAt;
            return customerDTO;
        }
    }


}