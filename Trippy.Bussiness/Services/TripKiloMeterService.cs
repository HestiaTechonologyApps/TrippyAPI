using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Core.Repositories;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.Domain.Interfaces.IServices;

namespace Trippy.Bussiness.Services
{
    public class TripKiloMeterService: ITripKiloMeterService
    {
        private readonly ITripKiloMeterRepository _repo;
        private readonly IAuditRepository _auditRepository;
        private readonly ICurrentUserService _currentUser;
        public String AuditTableName { get; set; } = "TRIPKILOMETER";
        public TripKiloMeterService(ITripKiloMeterRepository repo, IAuditRepository auditRepository, ICurrentUserService currentUser)
        {
            _repo = repo;
            this._auditRepository = auditRepository;
            _currentUser = currentUser;
        }

        public async Task<List<TripKiloMeterDTO>> GetAllAsync()
        {
            var tripkiloMeters = await _repo.GetAllTripKilometers();

            
            // List<TripKiloMeterDTO> tripkiloMeterDtos = new List<TripKiloMeterDTO>();
            // foreach (var tripkiloMeter in tripkiloMeters )
            // {
             //   TripKiloMeterDTO dto = await ConvertTripKiloMeterToDTO(tripkiloMeter);
               // tripkiloMeterDtos.Add(dto);
             //}
            return tripkiloMeters;

            
        }

        public async Task<TripKiloMeterDTO?> GetByIdAsync(int id)
        {
            var q = _repo.GetTripKilometerById(id); // already a DTO
            if (q == null) return null;

            return q;
        }

        public async Task<TripKiloMeterDTO> CreateAsync(TripKiloMeter tripKiloMeter)
        {
            await _repo.AddAsync(tripKiloMeter);
            await _repo.SaveChangesAsync();
            await this._auditRepository.LogAuditAsync<TripKiloMeter>(
               tableName: AuditTableName,
               action: "create",
               recordId: tripKiloMeter.TripKiloMeterId,
               oldEntity: null,
               newEntity: tripKiloMeter,
               changedBy: _currentUser.Email.ToString() // Replace with actual user info
           );
            return await ConvertTripKiloMeterToDTO(tripKiloMeter);
        }

        private async Task<TripKiloMeterDTO> ConvertTripKiloMeterToDTO(TripKiloMeter tripKiloMeter)
        {

            TripKiloMeterDTO tripkiloMeterDTO = new TripKiloMeterDTO();
            tripkiloMeterDTO.TripKiloMeterId = tripKiloMeter.TripKiloMeterId;
            tripkiloMeterDTO.TripOrderId = tripKiloMeter.TripOrderId;
            tripkiloMeterDTO.DriverId = tripKiloMeter.DriverId;
            tripkiloMeterDTO.VehicleId = tripKiloMeter.VehicleId;
            tripkiloMeterDTO.TripStartTime = tripKiloMeter.TripStartTime;
            tripkiloMeterDTO.TripEndTime = tripKiloMeter.TripEndTime;
            tripkiloMeterDTO.TripStartReading = tripKiloMeter.TripStartReading;
            tripkiloMeterDTO.TripEndReading = tripKiloMeter.TripEndReading;
            tripkiloMeterDTO.CreatedOn = tripKiloMeter.CreatedOn;
            tripkiloMeterDTO.WaitingHours = tripKiloMeter.WaitingHours;
            
            return tripkiloMeterDTO;
        }



        public async Task<List<TripKiloMeterDTO>> GetTripKilometerOfTrip(int TripOrderId)
        {

           // List<TripKiloMeterDTO> tripkmdto = new List<TripKiloMeterDTO>();

            var tripkms= await _repo.QuerableTripKiloMeters ().Where(u => u.TripOrderId == TripOrderId).ToListAsync();
           // var tripkms = await _repo.FindAsync(u => u.TripOrderId  == TripOrderId);
            //foreach (var kms in tripkms)
            //{
            //    TripKiloMeterDTO tripkmdtoobj = await ConvertTripKiloMeterToDTO(kms);
            //    tripkmdto.Add(tripkmdtoobj);


            //}
            return tripkms;
        }
        public async Task<bool> UpdateAsync(TripKiloMeter tripKiloMeter)
        {
            var oldentity = await _repo.GetByIdAsync(tripKiloMeter.TripKiloMeterId);
            _repo.Detach(oldentity);
            _repo.Update(tripKiloMeter);
            await _repo.SaveChangesAsync();
            await _auditRepository.LogAuditAsync<TripKiloMeter>(
               tableName: AuditTableName,
               action: "update",
               recordId: tripKiloMeter.TripKiloMeterId,
               oldEntity: oldentity,
               newEntity: tripKiloMeter,
               changedBy: _currentUser.Email.ToString() // Replace with actual user info
           );
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tripkiloMeter = await _repo.GetByIdAsync(id);
            if (tripkiloMeter == null) return false;
            _repo.Delete(tripkiloMeter);
            await _repo.SaveChangesAsync();
            await _auditRepository.LogAuditAsync<TripKiloMeter>(
               tableName: AuditTableName,
               action: "Delete",
               recordId: tripkiloMeter.TripKiloMeterId,
               oldEntity: tripkiloMeter,
               newEntity: tripkiloMeter,
               changedBy: _currentUser.Email.ToString() // Replace with actual user info
           );
            return true;
        }

       
    }
}
