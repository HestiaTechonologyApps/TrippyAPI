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

    public class TripNoteRepository : GenericRepository<TripNotes>, ITripNotesRepository
    {
        private readonly AppDbContext _context;
        public TripNoteRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

    }
       
}
