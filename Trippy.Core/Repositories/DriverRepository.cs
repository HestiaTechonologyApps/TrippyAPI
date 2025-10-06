using Trippy.Domain.Entities;
using Trippy.InfraCore.Data;
using TRIPPY.DOMAIN.Interfaces.IRepositories;

namespace Trippy.InfraCore.Repositories
{
    public class DriverRepository : GenericRepository<Driver>, IDriverRepository
    {
        private readonly AppDbContext _context;
        public DriverRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }

}
