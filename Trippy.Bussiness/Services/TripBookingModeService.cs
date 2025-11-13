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
        public String AuditTableName { get; set; } = "TRIPBOOKINGMODE";
        public TripBookingModeService(ITripBookingModeRepository repo, IAuditRepository auditRepository)
        {
            _repo = repo;
            this._auditRepository = auditRepository;
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

       
    }
}
