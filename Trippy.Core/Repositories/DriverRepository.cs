using Trippy.Domain.Entities;
using Trippy.InfraCore.Data;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.Domain.DTO;

namespace Trippy.InfraCore.Repositories
{
    public class DriverRepository : GenericRepository<Driver>, IDriverRepository
    {
        private readonly AppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        public DriverRepository(AppDbContext context, ICurrentUserService currentUser) : base(context)
        {
            _context = context;
            _currentUser = currentUser;
        }
        public async Task<IQueryable<DriverDTO>> GetQuerableDriverList()
        {
            var q = (from drv in _context.Drivers
                     join cmp in _context.Companies on drv.CompanyId equals cmp.CompanyId
                     where drv.CompanyId == int.Parse(_currentUser.CompanyId)
                     where drv.IsDeleted == false
                     select new DriverDTO
                     {
                         DriverId = drv.DriverId,

                         DriverName = drv.DriverName,
                         License = drv.License,
                         Nationality = drv.Nationality,

                         ProfileImagePath =drv.ProfileImagePath,

                         ContactNumber = drv.ContactNumber,
                         NationalId = drv.NationalId,
                          DOB=drv.DOB,
                         DOBString = drv.DOB.HasValue ? drv.DOB.Value.ToString("dd MMMM yyyy hh:mm tt") : "",
                         IsRented = drv.IsRented,
                         IsActive = drv.IsActive,
                         CompanyName = cmp.ComapanyName,
                         CompanyId = drv.CompanyId,
                         IsDeleted = drv.IsDeleted





                     }).AsQueryable();
            return q;
        }

    }

}
