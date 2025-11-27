using Microsoft.EntityFrameworkCore;
using Trippy.Domain.DTO;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.Domain.Interfaces.IServices;


namespace Trippy.Bussiness.Services
{
    public class ListService : IListService
    {
        private readonly ITripOrderRepository _repo;
        private readonly ICurrentUserService _currentUser;
        public ListService(ITripOrderRepository repo, ICurrentUserService currentUser)
        {
            _repo = repo;
            _currentUser = currentUser;
        }
        public async Task<PaginatedResult<TripListDataDTO>> Get_PaginatedTripList(
         string listType,
     string filtertext,
     int pagesize,
     int pagenumber, int CustomerId)
        {
            // normalize pages
            pagesize = pagesize <= 0 ? 10 : pagesize;
            pagenumber = pagenumber <= 0 ? 1 : pagenumber;

            var q = _repo.QuerableTripListAsyc();

            if((listType).ToLower() == "COMPLETED".ToLower ())
            {
                q = q.Where(t => t.Status.ToLower () == "Completed".ToLower ());
            }
            else if (listType.ToLower () == "Scheduled".ToLower())
            {
                q = q.Where(t => t.Status.ToLower() != "Scheduled".ToLower());
            }
            else if (listType.ToLower()    == "CANCELLED".ToLower())
            {
                q = q.Where(t => t.Status.ToLower() != "Cancelled".ToLower());
            }
            else if (listType.ToLower() == "TODAY".ToLower())
            {
                q = q.Where(t => t.FromDate.Value.Date== DateTime.Now.Date );
            }
            else if (listType.ToLower() == "UnInvoiced".ToLower())
            {
                q = q.Where(t => t.Status.ToLower() != "Completed".ToLower() && t.IsInvoiced==false );

                if (CustomerId != 0)
                {
                    q = q.Where(t => t.CustomerId != CustomerId);
                }

            }

            else if (listType.ToLower() == "ALL".ToLower())
            {
              
            }
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
