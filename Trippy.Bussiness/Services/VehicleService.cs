using Microsoft.EntityFrameworkCore;
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
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _repo;
        private readonly IAuditRepository _auditRepository;
        private readonly ICurrentUserService _currentUser;
        public String AuditTableName { get; set; } = "VEHICLE";
        public VehicleService(IVehicleRepository repo, IAuditRepository auditRepository, ICurrentUserService currentUser)
        {
            _repo = repo;
            this._auditRepository = auditRepository;
            _currentUser = currentUser;
        }
        public async Task<VehicleDTO> CreateAsync(Vehicle vehicle)
        {
            vehicle.CompanyId = int.Parse (_currentUser.CompanyId);
            await _repo.AddAsync(vehicle);
            await _repo.SaveChangesAsync();
            await this._auditRepository.LogAuditAsync<Vehicle>(
                tableName: AuditTableName,
                action: "create",
                recordId: vehicle.VehicleId,
                oldEntity: null,
                newEntity: vehicle,
                changedBy:_currentUser.Email.ToString() // Replace with actual user info
            );
            return await ConvertVehicleToDTO(vehicle);
        }

        private async Task<VehicleDTO> ConvertVehicleToDTO(Vehicle vehicle)
        {
            VehicleDTO vehicleDTO = new VehicleDTO();
            vehicleDTO.VehicleId = vehicle.VehicleId;
            vehicleDTO.RegistrationNumber = vehicle.RegistrationNumber;
            vehicleDTO.Make = vehicle.Make;
            vehicleDTO.Model = vehicle.Model;
            vehicleDTO.Year = vehicle.Year;
            vehicleDTO.ChassisNumber = vehicle.ChassisNumber;
            vehicleDTO.EngineNumber = vehicle.EngineNumber;
            vehicleDTO.VehicleType = vehicle.VehicleType;
            vehicleDTO.RegistrationExpiry = vehicle.RegistrationExpiry;
            
            vehicleDTO.CurrentStatus = vehicle.CurrentStatus;
            vehicleDTO.Location = vehicle.Location;
            vehicleDTO.CreatedDate = vehicle.CreatedDate;
            
            vehicleDTO.CreatedBy = vehicle.CreatedBy;
            vehicleDTO.UpdatedDate = vehicle.UpdatedDate;
            
            vehicleDTO.UpdatedBy = vehicle.UpdatedBy;
            vehicleDTO.CompanyId = vehicle.CompanyId;
            return vehicleDTO;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var vehicle = await _repo.GetByIdAsync(id);
            if (vehicle == null) return false;
            _repo.Delete(vehicle);
            await _repo.SaveChangesAsync();
            await _auditRepository.LogAuditAsync<Vehicle>(
                tableName: AuditTableName,
                action: "Delete",
                recordId: vehicle.VehicleId,
                oldEntity: vehicle,
                newEntity: vehicle,
                changedBy: _currentUser.Email.ToString() // Replace with actual user info
            );
            return true;
        }

        public async Task<List<VehicleDTO>> GetAllAsync()
        {


            List<VehicleDTO> vehicledtos = new List<VehicleDTO>();

            var vehicles = await _repo.GetQuerableVehicleList();

            //foreach (var vehicle in vehicles)
            //{
            //    VehicleDTO vehicleDTO = await ConvertVehicleToDTO(vehicle);
            //    vehicledtos.Add(vehicleDTO);


            //}

            return await vehicles.ToListAsync ();
        }
        public async Task<VehicleDTO?> GetByIdAsync(int id)
        {
            var q = await _repo.GetByIdAsync(id);
            if (q == null) return null;
            var vehicledto = await ConvertVehicleToDTO(q);
          

            return vehicledto;
        }

        public async Task<bool> UpdateAsync(Vehicle vehicle)
        {
            var oldentity = await _repo.GetByIdAsync(vehicle.VehicleId);
            _repo.Detach(oldentity);

            vehicle.CompanyId = int.Parse(_currentUser.CompanyId);
            _repo.Update(vehicle);
            await _repo.SaveChangesAsync();
            await _auditRepository.LogAuditAsync<Vehicle>(
               tableName: AuditTableName,
               action: "update",
               recordId: vehicle.VehicleId,
               oldEntity: oldentity,
               newEntity: vehicle,
               changedBy: _currentUser.Email.ToString() // Replace with actual user info
           );
            return true;
        }
    }
}
