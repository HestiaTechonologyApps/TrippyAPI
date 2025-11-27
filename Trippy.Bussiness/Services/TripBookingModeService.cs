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
    public class TripBookingModeService : ITripBookingModeService
    {
        private readonly ITripBookingModeRepository _repo;
        private readonly IAuditRepository _auditRepository;
        private readonly ICurrentUserService _currentUser;

        public String AuditTableName { get; set; } = "TRIPBOOKINGMODE";
        public TripBookingModeService(ITripBookingModeRepository repo, IAuditRepository auditRepository, ICurrentUserService currentUser)
        {
            _repo = repo;
            this._auditRepository = auditRepository;
            _currentUser = currentUser;
        }
        public async Task<List<TripBookingModeDTO>> GetAllAsync()
        {
            List<TripBookingModeDTO> tripBookingModes = new List<TripBookingModeDTO>();

            var tripBookings = await _repo.GetAllAsync();

            foreach (var tripbookingMode in tripBookings)
            {
               TripBookingModeDTO driverDTO = await ConvertTripBookingModeToDTO(tripbookingMode);
                tripBookingModes.Add(driverDTO);


            }

            return tripBookingModes ;
        }

       

        private async Task<TripBookingModeDTO> ConvertTripBookingModeToDTO(TripBookingMode tripBookingMode)
        {
            TripBookingModeDTO tripBookingModeDTO = new TripBookingModeDTO();
            tripBookingModeDTO.TripBookingModeId = tripBookingMode.TripBookingModeId;
            tripBookingModeDTO.TripBookingModeName = tripBookingMode.TripBookingModeName;
            return tripBookingModeDTO;
        }

        public async Task<TripBookingModeDTO?> GetByIdAsync(int id)
        {

            var q = await _repo.GetByIdAsync(id);
            if (q == null) return null;
            var tripBookingModeDTO = await ConvertTripBookingModeToDTO(q);
            return tripBookingModeDTO;
        }

        public async Task<TripBookingModeDTO> CreateAsync(TripBookingMode tripBookingMode)
        {
            await _repo.AddAsync(tripBookingMode);
            await _repo.SaveChangesAsync();
            await this._auditRepository.LogAuditAsync<TripBookingMode>(
               tableName: AuditTableName,
               action: "create",
               recordId: tripBookingMode.TripBookingModeId,
               oldEntity: null,
               newEntity: tripBookingMode,
               changedBy: _currentUser.Email.ToString() // Replace with actual user info
           );
            return await ConvertTripBookingModeToDTO(tripBookingMode);
        }

     

        public async Task<bool> UpdateAsync(TripBookingMode tripBookingMode)
        {
            var oldentity = await _repo.GetByIdAsync(tripBookingMode.TripBookingModeId);
            _repo.Detach(oldentity);
            _repo.Update(tripBookingMode);
            await _repo.SaveChangesAsync();
            await _auditRepository.LogAuditAsync<TripBookingMode>(
               tableName: AuditTableName,
               action: "update",
               recordId: tripBookingMode.TripBookingModeId,
               oldEntity: oldentity,
               newEntity: tripBookingMode,
               changedBy: _currentUser.Email.ToString() // Replace with actual user info
           );
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tripBookingMode = await _repo.GetByIdAsync(id);
            if (tripBookingMode == null) return false;
            _repo.Delete(tripBookingMode);
            await _repo.SaveChangesAsync();
            await _auditRepository.LogAuditAsync<TripBookingMode>(
               tableName: AuditTableName,
               action: "Delete",
               recordId: tripBookingMode.TripBookingModeId,
               oldEntity: tripBookingMode,
               newEntity: tripBookingMode,
               changedBy: _currentUser.Email.ToString() // Replace with actual user info
           );
            return true;
        }
    }
}
