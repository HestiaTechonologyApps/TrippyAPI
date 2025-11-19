using Microsoft.EntityFrameworkCore;
using Trippy.Domain.DTO;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.Domain.Interfaces.IServices;


namespace Trippy.Bussiness.Services
{
    public class ListService : IListService
    {
        private readonly ITripOrderRepository _repo;
        public ListService(ITripOrderRepository repo)
        {
            _repo = repo;

        }
     public async Task<PaginatedResult<TripListDataDTO>> Get_PaginatedTripList(
     string filtertext,
     int pagesize,
     int pagenumber)
        {
            // normalize pages
            pagesize = pagesize <= 0 ? 10 : pagesize;
            pagenumber = pagenumber <= 0 ? 1 : pagenumber;

            var q = _repo.QuerableTripListAsyc();

            // filtering
            if (!string.IsNullOrWhiteSpace(filtertext))
            {
                q = q.Where(t =>
                    t.CustomerName.Contains(filtertext) ||
                    t.DriverName.Contains(filtertext) ||
                    t.PickUpFrom.Contains(filtertext) ||
                    t.RecivedVia.Contains(filtertext) ||
                    t.Status.Contains(filtertext) ||
                    t.TripCode.Contains(filtertext)
                );
            }

            // total BEFORE pagination
            var total = await q.CountAsync();

            // pagination
            var data = await q
                .Skip((pagenumber - 1) * pagesize)
                .Take(pagesize)
                .ToListAsync();

            return new PaginatedResult<TripListDataDTO>
            {
                Total = total,
                Data = data
            };
        }
    }


}
