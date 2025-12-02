using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.Domain.Interfaces.IServices;

namespace Trippy.Bussiness.Services
{
    public class CustomerDepartmentService : ICustomerDepartmentService
    {
        private readonly ICustomerDepartmentRepository _repo;
        private readonly IAuditRepository _auditRepository;
        private readonly ICurrentUserService _currentUser;
        public String AuditTableName { get; set; } = "CUSTOMERdEPARTMENT";

        public CustomerDepartmentService(ICustomerDepartmentRepository repo, IAuditRepository auditRepository, ICurrentUserService currentUser)
        {
            _repo = repo;
            this._auditRepository = auditRepository;
            _currentUser = currentUser;
        }

        public async Task<List<CustomerDepartmentDTO>> GetAllAsync()
        {
             List<CustomerDepartmentDTO> driverdtos = new List<CustomerDepartmentDTO>();

            var customerDepartments = await _repo.GetAllAsync();

            foreach (var customerDepartment in customerDepartments)
            {
                CustomerDepartmentDTO customerDepartmentDTO = await ConvertToCustomerDepartmentToDTO(customerDepartment);
                driverdtos.Add(customerDepartmentDTO);


            }

            return driverdtos;
        }

        public async Task<CustomerDepartmentDTO?> GetByIdAsync(int id)
        {
            var q = await _repo.GetByIdAsync(id);
            if (q == null) return null;
            var customerDepartmentDTO = await ConvertToCustomerDepartmentToDTO(q);
            return customerDepartmentDTO;
        }

        public async Task<CustomerDepartmentDTO> CreateAsync(CustomerDepartment customerDepartment)
        {
            await _repo.AddAsync(customerDepartment);
            await _repo.SaveChangesAsync();
            await this._auditRepository.LogAuditAsync<CustomerDepartment>(
               tableName: AuditTableName,
               action: "create",
               recordId: customerDepartment.DepartmentId,
               oldEntity: null,
               newEntity: customerDepartment,
               changedBy: _currentUser.Email.ToString()
// Replace with actual user info

);
            return await ConvertToCustomerDepartmentToDTO(customerDepartment);
        }

        private async Task<CustomerDepartmentDTO> ConvertToCustomerDepartmentToDTO(CustomerDepartment customerDepartment)
        {
            CustomerDepartmentDTO customerDepartmentDTO = new CustomerDepartmentDTO();
            customerDepartmentDTO.DepartmentId = customerDepartment.DepartmentId;
            customerDepartmentDTO.DepartmentName = customerDepartment.DepartmentName;
            customerDepartmentDTO.DepartmentDescription = customerDepartment.DepartmentDescription;
            return customerDepartmentDTO;
        }

        public async Task<bool> UpdateAsync(CustomerDepartment customerDepartment)
        {
            var oldentity = await _repo.GetByIdAsync(customerDepartment.DepartmentId);
            _repo.Update(customerDepartment);
            await _repo.SaveChangesAsync();
            await this._auditRepository.LogAuditAsync<CustomerDepartment>(
               tableName: AuditTableName,
               action: "update",
               recordId: customerDepartment.DepartmentId,
               oldEntity: oldentity,
               newEntity: customerDepartment,
               changedBy: _currentUser.Email.ToString() // Replace with actual user info
               );
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customerDepartment = await _repo.GetByIdAsync(id);

            if (customerDepartment == null) return false;
            _repo.Delete(customerDepartment);
            await _repo.SaveChangesAsync();
            await _auditRepository.LogAuditAsync<CustomerDepartment>(
               tableName: AuditTableName,
               action: "Delete",
               recordId: customerDepartment.DepartmentId,
               oldEntity: customerDepartment,
               newEntity: customerDepartment,
               changedBy: _currentUser.Email.ToString() // Replace with actual user info
           );
            return true;

        }
    }
}
