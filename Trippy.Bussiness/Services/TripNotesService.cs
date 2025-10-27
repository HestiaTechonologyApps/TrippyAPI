using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.Domain.Interfaces.IServices;

namespace Trippy.Bussiness.Services
{
    public class TripNotesService : ITripNoteService
    {
        private readonly ITripNotesRepository _repo;
        private readonly IAuditRepository _auditRepository;

        public TripNotesService(ITripNotesRepository repo, IAuditRepository auditRepository)
        {
            _repo = repo;
            this._auditRepository = auditRepository;
        }
        public String AuditTableName { get; set; } = "TRIPNOTES";
        public async Task<TripNotesDTO> CreateAsync(TripNotes tripNotes)
        {
            await _repo.AddAsync(tripNotes);
            await _repo.SaveChangesAsync();
            await this._auditRepository.LogAuditAsync<TripNotes>(
                tableName: AuditTableName,
                action: "create",
                recordId: tripNotes.TripNoteId,
                oldEntity: null,
                newEntity: tripNotes,
                changedBy: "System"
                );
            return await ConvertTripNotesToDTO(tripNotes);
        }

        private async Task<TripNotesDTO> ConvertTripNotesToDTO(TripNotes tripNotes)
        {
            TripNotesDTO tripNotesDTO = new TripNotesDTO();
            tripNotesDTO.TripNoteId = tripNotes.TripNoteId;
            tripNotesDTO.TripNote = tripNotes.TripNote;
            tripNotesDTO.CreatedOn = tripNotes.CreatedOn;
            tripNotesDTO.CtreatedOnString = tripNotes.CreatedOn.ToString("dd MMMM yyyy hh:mm tt");
            //  tripNotesDTO.AuditLogg = await _auditRepository.GetAuditLogsForEntityAsync
            return tripNotesDTO;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tripNotes = _repo.GetByIdAsync(id).Result;
            if (tripNotes == null) return false;
            _repo.Delete(tripNotes);
            await _repo.SaveChangesAsync();
            return true;

        }

        public async Task<List<TripNotesDTO>> GetAllAsync()
        {
            List<TripNotesDTO> tripNotesDTOs = new List<TripNotesDTO>();
            var tripNotes = await _repo.GetAllAsync();
            foreach (var tripNote in tripNotes)
            {
                TripNotesDTO tripNotesDTO = await ConvertTripNotesToDTO(tripNote);
                tripNotesDTOs.Add(tripNotesDTO);


            }
            return tripNotesDTOs;
        }

        public async Task<TripNotesDTO?> GetByIdAsync(int id)
        {
            var q = await _repo.GetByIdAsync(id);
            if (q == null) return null;
            var tripNotesDTOS = await ConvertTripNotesToDTO(q);
            return tripNotesDTOS;
          
        }

        public async Task<bool> UpdateAsync(TripNotes tripNotes)
        {
            _repo.Update(tripNotes);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<List<TripNotesDTO>> GetTripNotesOfTrip(int id)
        {
            
            List<TripNotesDTO> tripNotesDTOs = new List<TripNotesDTO>();
            var tripNotes = await _repo.FindAsync(u => u.TripNoteId == id);
            foreach (var tripNote in tripNotes)
            {
                TripNotesDTO tripNotesDTO = await ConvertTripNotesToDTO(tripNote);
                tripNotesDTOs.Add(tripNotesDTO);


            }
            return tripNotesDTOs;
        }
    }  
}
