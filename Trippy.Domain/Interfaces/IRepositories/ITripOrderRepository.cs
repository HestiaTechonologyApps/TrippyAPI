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
       Task< TripOrderDTO> GetTripDetails(int tripid);
        Task<List<TripListDataDTO>> GetTripListAsync();
       
        Task<List<TripListDataDTO>> GetAllByStatusAndYearAsync(string status, int year);



        Task<IEnumerable<TripListDataDTO>> GetCanceledTripsAsync();
     
      
        Task<int> GetTotalTripsAsync(int year);
        Task<int> GetTripCountByStatusAsync(int CompanyId,int year,string ststus);
        Task<int> GetTripCountByStatusAndDateRangeAsync(string status, DateTime startDate, DateTime endDate);
       // Task<List<TripOrderDTO>> GetAllTripsDetailAsync();
        Task<List<TripListDataDTO>> GetTodaysTripsAsync(DateTime today);
        Task<List<TripListDataDTO>> GetTripsByDateAsync(DateTime date);
        Task<int> GetTodaysTripsCountAsync(DateTime today);
        Task<List<TripListDataDTO>> GetAllTripsAsync();

        IQueryable<TripListDataDTO> QuerableTripListAsyc();

         Task<List<CalendarEventDto>> GetDriverSchedule(int driverId, DateTime start, DateTime end);



    }

}
