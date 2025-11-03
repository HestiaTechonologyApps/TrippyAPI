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
    public class VehicleMainteanceRepository : GenericRepository<VehicleMaintenanceRecord>, IVehicleMaintanenceRepository
    {
        private readonly AppDbContext _context;
        public VehicleMainteanceRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
