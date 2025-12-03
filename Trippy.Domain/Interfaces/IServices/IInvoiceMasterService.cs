using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;

namespace Trippy.Domain.Interfaces.IServices
{
    public interface IInvoiceMasterService
    {
        Task<List<InvoiceMasterDTO>> GetAllAsync();
        Task<InvoiceMasterDTO?> GetByIdAsync(int id);
        Task<InvoiceMasterDTO> CreateAsync(InvoiceMasterDTO  invoiceMaster);
        Task<bool> UpdateAsync(InvoiceMaster invoiceMaster);
        Task<bool> DeleteAsync(int id);
        Task<List<TripDashboardDTO>> GetAllInvoicDashboardListbyStatusAsync(int year);

        Task<CustomApiResponse> GenerateInvoice(InvoiceMasterIdList invoiceMasterIdList);


    }
}
