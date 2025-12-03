using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.DTO;
using Trippy.Domain.Entities;

namespace Trippy.Domain.Interfaces.IRepositories
{
    public interface IInvoiceMasterRepository : IGenericRepository<InvoiceMaster>
    {

        IQueryable<InvoiceMasterDTO> QuerableInvoiceMasterListAsyc();


    }
}
