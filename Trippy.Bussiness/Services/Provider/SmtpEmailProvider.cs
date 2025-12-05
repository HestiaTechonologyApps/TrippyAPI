using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.Interfaces.IServices;

namespace Trippy.Bussiness.Services.Provider
{
    public class SmtpEmailProvider : IEmailProvider
    {
        private readonly IConfiguration _config;

        public SmtpEmailProvider(IConfiguration config) => _config = config;

        public async Task SendEmailAsync(string to, string subject, string body, CancellationToken ct = default)
        {
            var host = _config["Email:Smtp:Host"];
            var port = int.Parse(_config["Email:Smtp:Port"] ?? "587");
            var user = _config["Email:Smtp:User"];
            var pass = _config["Email:Smtp:Pass"];
            var from = _config["Email:Smtp:From"];

            using var client = new SmtpClient(host, port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(user, pass)
            };

            using var mail = new MailMessage(from, to, subject, body)
            {
                IsBodyHtml = true
            };

            // SmtpClient.SendMailAsync has a CancellationToken overload only in newer runtimes;
            // we respect ct by registering cancellation to the Task if needed.
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            await client.SendMailAsync(mail, cts.Token);
        }
    }
}
