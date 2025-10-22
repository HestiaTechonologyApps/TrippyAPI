using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;

namespace Trippy.Domain.Interfaces.IServices
{
    public interface ITripNoteService
    {

      
        Task<List<TripNotesDTO>> GetAllAsync();
        Task<TripNotesDTO?> GetByIdAsync(int id);
        Task<TripNotesDTO> CreateAsync(TripNotes tripNotes);

        Task<List<TripNotesDTO>> GetTripNotesOfTrip(int id);
        Task<bool> UpdateAsync(TripNotes tripNotes);
        Task<bool> DeleteAsync(int id);
    }
}
