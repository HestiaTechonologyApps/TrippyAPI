using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trippy.Domain.Entities;
using Trippy.Domain.Interfaces;
using Trippy.Domain.Interfaces.IRepositories;
using Trippy.Domain.Interfaces.IServices;

namespace Trippy.Bussiness.Services
{
    public class NotificationService:INotificationService
    {
        private readonly INotificationRepository     _repo;
        public NotificationService(INotificationRepository repo)
        {
            _repo = repo;
        }

        public async Task QueueNotificationAsync(NotificationRequest request, CancellationToken ct = default)
        {
            var entity = new NotificationQueue
            {
                Recipient = request.Recipient,
                Subject = request.Subject,
                Body = request.Body,
                Channel = request.Channel,
                ScheduledAt = request.ScheduledAt
            };

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

           
        }
    }
}
