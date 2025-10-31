using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;

namespace Trippy.Domain.Interfaces.IRepositories
{
    public interface ITripOrderRepository:IGenericRepository<TripOrder>
    {
        TripOrderDTO GetTripDetails(int tripid);
        IEnumerable<TripListDataDTO> GetTripListAsync();
        Task<IEnumerable<TripOrderDTO>> GetCanceledTripsAsync();
        Task<IEnumerable<TripOrder>> GetAllByStatusAsync(string status);

        Task<int> GetTotalTripsAsync();
        Task<int> GetTripCountByStatusAsync(string ststus);
        Task<int> GetTripCountByStatusAndDateRangeAsync(string status, DateTime startDate, DateTime endDate);

        Task<List<TripOrder>> GetTodaysTripsAsync(DateTime today);
        Task<List<TripOrder>> GetTripsByDateAsync(DateTime date);
        Task<int> GetTodaysTripsCountAsync(DateTime today);
    }
    
}
