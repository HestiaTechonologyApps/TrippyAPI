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
        private readonly ICurrentUserService _currentUser;
        public VehicleRepository(AppDbContext context, ICurrentUserService currentUser) : base(context)
        {
            _context = context;
            _currentUser = currentUser;
        }
        public async Task<IQueryable<VehicleDTO>> GetQuerableVehicleList()
        {
            var q = (from vech in _context.Vehicles
                     join cmp in _context.Companies
                     on vech.CompanyId equals cmp.CompanyId
                     where vech.CompanyId == int.Parse(_currentUser.CompanyId)
                     where vech.IsDeleted == false
                     select new VehicleDTO
                     {
                         VehicleId = vech.VehicleId,
                         VehicleName = vech.RegistrationNumber,
                         VehicleType = vech.VehicleType,
                         Make = vech.Make,
                         Model = vech.Model,
                         Year = vech.Year,
                         ComapanyName = cmp.ComapanyName,
                         ChassisNumber = vech.ChassisNumber,
                         EngineNumber = vech.EngineNumber,
                         RegistrationExpiry = vech.RegistrationExpiry,
                         CreatedBy = vech.CreatedBy,
                         CreatedDate = vech.CreatedDate,

                         RegistrationNumber = vech.RegistrationNumber,
                         Location = vech.Location,
                         CurrentStatus = vech.CurrentStatus,
                         CompanyId = vech.CompanyId,
                         IsDeleted = vech.IsDeleted,
                     }).AsQueryable();

            return q;
        }
    }

    public class NotificationRepository : GenericRepository<NotificationQueue>, INotificationRepository
    {
        private readonly AppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        public NotificationRepository(AppDbContext context, ICurrentUserService currentUser) : base(context)
        {
            _context = context;
            _currentUser = currentUser;
        }

    }
}
