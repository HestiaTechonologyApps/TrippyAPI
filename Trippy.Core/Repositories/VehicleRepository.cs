using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
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
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        private readonly AppDbContext _context;
        public VehicleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IQueryable<VehicleDTO>> GetQuerableVehicleList() {
            var q = (from vech in _context.Vehicles
                     join cmp in _context.Companies
                     on vech.CompanyId equals cmp.CompanyId
                     select new VehicleDTO
                     {
                         VehicleId = vech.VehicleId,
                         VehicleName = vech.RegistrationNumber,
                         VehicleType = vech.VehicleType,
                         Make = vech.Make,
                         Model = vech.Model,
                         Year = vech.Year,
                         CompanyName = cmp.ComapanyName,
                         ChassisNumber = vech.ChassisNumber,
                         EngineNumber = vech.EngineNumber,
                         RegistrationExpiry = vech.RegistrationExpiry

                     }).AsQueryable();

            return q;
        }
    }
}
