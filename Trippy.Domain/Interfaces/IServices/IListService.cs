using Trippy.Domain.DTO;

namespace Trippy.Domain.Interfaces.IServices
{
    public interface IListService
    {
        Task<PaginatedResult<TripListDataDTO>> Get_PaginatedTripList(
          string listType,
      string filtertext,
      int pagesize,
      int pagenumber, int CustomerId,int Year);

    }
}
