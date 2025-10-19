using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.InfraCore.Data;

namespace Trippy.InfraCore.Repositories
{
    public class AttachmentRepository : GenericRepository<Attachment>, IAttachmentRepository
    {
        private readonly AppDbContext _context;
        public AttachmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }

}
