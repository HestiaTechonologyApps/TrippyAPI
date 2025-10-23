using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;

namespace Trippy.Domain.Interfaces.IServices
{
    public interface ITripOrderService
    {
        Task<List<TripOrderDTO>> GetAllAsync();
        Task<List<TripOrderDTO>> GetAllTripListbyStatusAsync(string Status);


        IEnumerable<TripListDataDTO> GetAll();
        Task<TripOrderDTO?> GetByIdAsync(int id);
        Task<TripOrderDTO> CreateAsync(TripOrder coupon);
        Task<bool> UpdateAsync(TripOrder coupon);
        Task<bool> DeleteAsync(int id);
    }
}
