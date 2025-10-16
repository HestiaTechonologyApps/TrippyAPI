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
        Task<List<invoiceMasterDTO>> GetAllAsync();
        Task<invoiceMasterDTO?> GetByIdAsync(int id);
        Task<invoiceMasterDTO> CreateAsync(InvoiceMaster invoiceMaster);
        Task<bool> UpdateAsync(InvoiceMaster invoiceMaster);
        Task<bool> DeleteAsync(int id);
    }
}
