using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;

namespace Trippy.Domain.Interfaces.IServices
{
    public interface ITripKiloMeterService
    {
        Task<List<TripKiloMeterDTO>> GetAllAsync();
        
        Task<TripKiloMeterDTO?> GetByIdAsync(int id);
        Task<TripKiloMeterDTO> CreateAsync(TripKiloMeter tripKiloMeter);
        Task<bool> UpdateAsync(TripKiloMeter tripKiloMeter);
        Task<bool> DeleteAsync(int id);
        Task<List<TripKiloMeterDTO>> GetTripKilometerOfTrip(int TripOrderId);
    }
}
