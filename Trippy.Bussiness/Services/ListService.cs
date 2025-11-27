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
    int pagenumber,
    int CustomerId,
    int Year)
        {
            // Normalize pagination parameters
            pagesize = Math.Max(pagesize, 1);  // Cleaner than ternary
            pagesize = Math.Min(pagesize, 100); // Add max limit
            pagenumber = Math.Max(pagenumber, 1);

            var q = _repo.QuerableTripListAsyc();

            // Filter by Customer
            if (CustomerId > 0)
            {
                q = q.Where(t => t.CustomerId == CustomerId);
            }

            // Filter by Year
            if (Year > 0)
            {
                q = q.Where(t => t.FromDate != null && t.FromDate.Value.Year == Year);
            }

            // Filter by List Type (using StringComparison for better performance)
            if (!string.IsNullOrWhiteSpace(listType))
            {
                switch (listType.ToLower())
                {
                    case "completed":
                        q = q.Where(t => EF.Functions.Like(t.Status, "completed"));
                        break;

                    case "scheduled":
                        q = q.Where(t => EF.Functions.Like(t.Status, "scheduled"));
                        break;

                    case "cancelled":
                        q = q.Where(t => EF.Functions.Like(t.Status, "cancelled"));
                        break;

                    case "today":
                        var today = DateTime.Today;
                        q = q.Where(t => t.FromDate != null && t.FromDate.Value.Date == today);
                        break;

                    case "uninvoiced":
                        q = q.Where(t => EF.Functions.Like(t.Status, "completed") && t.IsInvoiced == false);
                        break;

                    case "all":
                    default:
                        // No filtering
                        break;
                }
            }

            // Text filtering (case-insensitive using EF.Functions)
            if (!string.IsNullOrWhiteSpace(filtertext))
            {
                var pattern = $"%{filtertext}%";
                q = q.Where(t =>
                    EF.Functions.Like(t.CustomerName, pattern) ||
                    EF.Functions.Like(t.DriverName, pattern) ||
                    EF.Functions.Like(t.PickUpFrom, pattern) ||
                    EF.Functions.Like(t.RecivedVia, pattern) ||
                    EF.Functions.Like(t.Status, pattern) ||
                    EF.Functions.Like(t.TripCode, pattern)
                );
            }

            // Get total count
            var total = await q.CountAsync();

            // Apply pagination with ordering (IMPORTANT!)
            var data = await q
                .OrderByDescending(t => t.FromDate)  // ADD ORDERING!
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
