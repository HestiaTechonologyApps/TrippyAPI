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
        Task<List<TripListDataDTO>> GetAllTripListbyStatusAsync(string Status);

        Task<List<TripDashboardDTO>> GetAllTripDashboardListbyStatusAsync();

        Task<int> GetTodaysTripCountAsync();
        Task <List<TripListDataDTO>> GetAll();
        Task<IEnumerable<TripOrderDTO>> GetCanceledTripsAsync();
        Task<List<TripOrderDTO>> GetTodaysTripListAsync();
        Task<List<TripOrderDTO>> GetTripsByDateAsync(DateTime date);

        Task<List<TripListDataDTO>> GetAllTripListByYearAsync(int year);
        Task<List<TripListDataDTO>> GetAllTripListByStatusAndYearAsync(string status, int year);

       Task<List<TripListDataDTO>> GetAllTripListAsync(string? status = null, int? year = null);

        Task<TripOrderDTO?> GetByIdAsync(int id);
        Task<TripOrderDTO> CreateAsync(TripOrder coupon);
        Task<bool> UpdateAsync(TripOrder coupon);
        Task<bool> DeleteAsync(int id);
    }
}
