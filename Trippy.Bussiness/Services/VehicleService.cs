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
        public String AuditTableName { get; set; } = "VEHICLE";
        public VehicleService(IVehicleRepository repo, IAuditRepository auditRepository)
        {
            _repo = repo;
            this._auditRepository = auditRepository;
        }
        public async Task<VehicleDTO> CreateAsync(Vehicle vehicle)
        {
            await _repo.AddAsync(vehicle);
            await _repo.SaveChangesAsync();
            await this._auditRepository.LogAuditAsync<Vehicle>(
                tableName: AuditTableName,
                action: "create",
                recordId: vehicle.VehicleId,
                oldEntity: null,
                newEntity: vehicle,
                changedBy: "System" // Replace with actual user info
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
            vehicleDTO.RegistrationExpiryString = vehicle.RegistrationExpiry.ToString("yyyy-MM-dd");
            vehicleDTO.CurrentStatus = vehicle.CurrentStatus;
            vehicleDTO.Location = vehicle.Location;
            vehicleDTO.CreatedDate = vehicle.CreatedDate;
            vehicleDTO.CreatedDateString = vehicle.CreatedDate.ToString("yyyy-MM-dd");
            vehicleDTO.CreatedBy = vehicle.CreatedBy;
            vehicleDTO.UpdatedDate = vehicle.UpdatedDate;
            vehicleDTO.UpdatedDateString = vehicle.UpdatedDate.HasValue ? vehicle.UpdatedDate.Value.ToString("yyyy-MM-dd") : "";
            vehicleDTO.UpdatedBy = vehicle.UpdatedBy;
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
                changedBy: "System" // Replace with actual user info
            );
            return true;
        }

        public async Task<List<VehicleDTO>> GetAllAsync()
        {


            List<VehicleDTO> vehicledtos = new List<VehicleDTO>();

            var vehicles = await _repo.GetAllAsync();

            foreach (var vehicle in vehicles)
            {
                VehicleDTO vehicleDTO = await ConvertVehicleToDTO(vehicle);
                vehicledtos.Add(vehicleDTO);


            }

            return vehicledtos;
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
            _repo.Update(vehicle);
            await _repo.SaveChangesAsync();
            await _auditRepository.LogAuditAsync<Vehicle>(
               tableName: AuditTableName,
               action: "update",
               recordId: vehicle.VehicleId,
               oldEntity: oldentity,
               newEntity: vehicle,
               changedBy: "System" // Replace with actual user info
           );
            return true;
        }
    }
}
