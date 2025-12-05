using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trippy.Domain.Interfaces.IServices
{
    public interface IEmailProvider
    {
        Task SendEmailAsync(string to, string subject, string body, CancellationToken ct = default);
    }
}
