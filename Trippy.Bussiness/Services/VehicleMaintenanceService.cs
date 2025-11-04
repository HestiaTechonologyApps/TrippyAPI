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
    public class VehicleMaintenanceService : IVehicleMaintenanceRecordService
    {
        private readonly IVehicleMaintanenceRepository _repo;
        private readonly IAuditRepository _auditRepository;
        public String AuditTableName { get; set; } = "VEHICLEMAINTENANCERECORD";

        public VehicleMaintenanceService(IVehicleMaintanenceRepository repo, IAuditRepository auditRepository)
        {
            _repo = repo;
            this._auditRepository = auditRepository;
        }

        public async Task<List<VehicleMaintenanceRecordDTO>> GetAllAsync()
        {
           // List<VehicleMaintenanceRecordDTO> vehicleMaintenanceRecorddtos = new List<VehicleMaintenanceRecordDTO>();

            var vehicleMaintenanceRecords =  _repo.GetAllExpenses();

          //  foreach (var vehicleMaintenanceRecord in vehicleMaintenanceRecords)
           // {
              //  VehicleMaintenanceRecordDTO vehicleMaintenanceRecordDTO = await ConvertVehicleMaintenanceRecordToDTO(vehicleMaintenanceRecord);
                //vehicleMaintenanceRecorddtos.Add(vehicleMaintenanceRecordDTO);


          //  }

            return vehicleMaintenanceRecords;
        }

        private async Task<VehicleMaintenanceRecordDTO> ConvertVehicleMaintenanceRecordToDTO(VehicleMaintenanceRecord vehicleMaintenanceRecord)
        {
            VehicleMaintenanceRecordDTO vehicleMaintenanceRecordDTO = new VehicleMaintenanceRecordDTO();
            vehicleMaintenanceRecordDTO.VehicleMaintenanceRecordId = vehicleMaintenanceRecord.VehicleMaintenanceRecordId;
            vehicleMaintenanceRecordDTO.VehicleId = vehicleMaintenanceRecord.VehicleId;
            vehicleMaintenanceRecordDTO.MaintenanceDate = vehicleMaintenanceRecord.MaintenanceDate;
            vehicleMaintenanceRecordDTO.MaintenanceDateString = vehicleMaintenanceRecord.MaintenanceDate.ToString("dd MMMM yyyy hh:mm tt");
            vehicleMaintenanceRecordDTO.MaintenanceType = vehicleMaintenanceRecord.MaintenanceType;
            vehicleMaintenanceRecordDTO.WorkshopName = vehicleMaintenanceRecord.WorkshopName;
            vehicleMaintenanceRecordDTO.Description = vehicleMaintenanceRecord.Description;
            vehicleMaintenanceRecordDTO.Cost = vehicleMaintenanceRecord.Cost;
            vehicleMaintenanceRecordDTO.OdometerReading = vehicleMaintenanceRecord.OdometerReading;
            vehicleMaintenanceRecordDTO.PerformedBy = vehicleMaintenanceRecord.PerformedBy;
            vehicleMaintenanceRecordDTO.Remarks = vehicleMaintenanceRecord.Remarks;
            vehicleMaintenanceRecordDTO.CreatedDate = vehicleMaintenanceRecord.CreatedDate;
            vehicleMaintenanceRecordDTO.CreatedDateString = vehicleMaintenanceRecord.CreatedDate.ToString("dd MMMM yyyy hh:mm tt");
            vehicleMaintenanceRecordDTO.CreatedBy = vehicleMaintenanceRecord.CreatedBy;
            return vehicleMaintenanceRecordDTO;
        }

        public async Task<VehicleMaintenanceRecordDTO?> GetByIdAsync(int id)
        {
            var q = await _repo.GetByIdAsync(id);
            if (q == null) return null;
            var vehicleMaintenanceRecorddto = await ConvertVehicleMaintenanceRecordToDTO(q);


            return vehicleMaintenanceRecorddto;
        }

        public async Task<VehicleMaintenanceRecordDTO> CreateAsync(VehicleMaintenanceRecord vehicleMaintenanceRecord)
        {
            await _repo.AddAsync(vehicleMaintenanceRecord);
            await _repo.SaveChangesAsync();
            await this._auditRepository.LogAuditAsync<VehicleMaintenanceRecord>(
                tableName: AuditTableName,
                action: "create",
                recordId: vehicleMaintenanceRecord.VehicleMaintenanceRecordId,
                oldEntity: null,
                newEntity: vehicleMaintenanceRecord,
                changedBy: "System" // Replace with actual user info
            );
            return await ConvertVehicleMaintenanceRecordToDTO(vehicleMaintenanceRecord);
        }

        public async Task<bool> UpdateAsync(VehicleMaintenanceRecord vehicleMaintenanceRecord)
        {
            var oldentity = await _repo.GetByIdAsync(vehicleMaintenanceRecord.VehicleMaintenanceRecordId);
            _repo.Detach(oldentity);
            _repo.Update(vehicleMaintenanceRecord);
            await _repo.SaveChangesAsync();
            await _auditRepository.LogAuditAsync<VehicleMaintenanceRecord>(
               tableName: AuditTableName,
               action: "update",
               recordId: vehicleMaintenanceRecord.VehicleMaintenanceRecordId,
               oldEntity: oldentity,
               newEntity: vehicleMaintenanceRecord,
               changedBy: "System" // Replace with actual user info
           );
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var vehicleMaintenanceRecord = await _repo.GetByIdAsync(id);
            if (vehicleMaintenanceRecord == null) return false;
            _repo.Delete(vehicleMaintenanceRecord);
            await _repo.SaveChangesAsync();
            await _auditRepository.LogAuditAsync<VehicleMaintenanceRecord>(
                tableName: AuditTableName,
                action: "Delete",
                recordId: vehicleMaintenanceRecord.VehicleMaintenanceRecordId,
                oldEntity: vehicleMaintenanceRecord,
                newEntity: vehicleMaintenanceRecord,
                changedBy: "System" // Replace with actual user info
            );
            return true;
        }
    }
}
