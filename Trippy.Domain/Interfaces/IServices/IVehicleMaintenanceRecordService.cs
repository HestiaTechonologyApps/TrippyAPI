using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;

namespace Trippy.Domain.Interfaces.IServices
{
    public interface IVehicleMaintenanceRecordService
    {
        Task<List<VehicleMaintenanceRecordDTO>> GetAllAsync();
        Task<VehicleMaintenanceRecordDTO?> GetByIdAsync(int id);
        Task<VehicleMaintenanceRecordDTO> CreateAsync(VehicleMaintenanceRecord coupon);
        Task<bool> UpdateAsync(VehicleMaintenanceRecord coupon);
        Task<bool> DeleteAsync(int id);
    }
}
