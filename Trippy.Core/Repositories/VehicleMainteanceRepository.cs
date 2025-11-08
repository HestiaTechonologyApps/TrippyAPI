using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.InfraCore.Data;

namespace Trippy.Core.Repositories
{
    public class VehicleMainteanceRepository : GenericRepository<VehicleMaintenanceRecord>, IVehicleMaintanenceRepository
    {
        private readonly AppDbContext _context;
        public VehicleMainteanceRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

       

        public List<VehicleMaintenanceRecordDTO> GetAllExpenses()
        {
            var result = (from vm in _context.VehicleMaintenanceRecords
                          join veh in _context.Vehicles on vm.VehicleId equals veh.VehicleId into vehGroup
                          from veh in vehGroup.DefaultIfEmpty()
                          select new VehicleMaintenanceRecordDTO
                          {
                              VehicleMaintenanceRecordId = vm.VehicleMaintenanceRecordId,
                              VehicleId = vm.VehicleId,
                              VehicleName = veh != null ? veh.VehicleType + " - " + veh.RegistrationNumber : "N/A",
                              WorkshopName = vm.WorkshopName,
                              Description = vm.Description,
                              Cost = vm.Cost,
                              OdometerReading = vm.OdometerReading,
                              PerformedBy = vm.PerformedBy,
                              CreatedBy = vm.CreatedBy,
                              CreatedDate = vm.CreatedDate,
                              CreatedDateString = vm.CreatedDate.ToString("dd MMMM yyyy hh:mm tt"),
                              MaintenanceDate = vm.MaintenanceDate,
                              MaintenanceType = vm.MaintenanceType,
                              MaintenanceDateString = vm.MaintenanceDate.ToString("dd MMMM yyyy hh:mm tt"),
                              Remarks = vm.Remarks
                          }).ToList();

            return result;
        }

        public async Task<VehicleMaintenanceRecordDTO?> GetExpenseByIdAsync(int id)
        {
            var result = await(from vm in _context.VehicleMaintenanceRecords
                               join veh in _context.Vehicles on vm.VehicleId equals veh.VehicleId into vehGroup
                               from veh in vehGroup.DefaultIfEmpty()
                               where vm.VehicleMaintenanceRecordId == id
                               select new VehicleMaintenanceRecordDTO
                               {
                                   VehicleMaintenanceRecordId = vm.VehicleMaintenanceRecordId,
                                   VehicleId = vm.VehicleId,
                                   VehicleName = veh != null ? veh.VehicleType + " - " + veh.RegistrationNumber : "N/A",
                                   WorkshopName = vm.WorkshopName,
                                   Description = vm.Description,
                                   Cost = vm.Cost,
                                   OdometerReading = vm.OdometerReading,
                                   PerformedBy = vm.PerformedBy,
                                   CreatedBy = vm.CreatedBy,
                                   CreatedDate = vm.CreatedDate,
                                   CreatedDateString = vm.CreatedDate.ToString("dd MMMM yyyy hh:mm tt"),
                                   MaintenanceDate = vm.MaintenanceDate,
                                   MaintenanceType = vm.MaintenanceType,
                                   MaintenanceDateString = vm.MaintenanceDate.ToString("dd MMMM yyyy hh:mm tt"),
                                   Remarks = vm.Remarks
                               }).FirstOrDefaultAsync();

            return result;
        }
    }
}