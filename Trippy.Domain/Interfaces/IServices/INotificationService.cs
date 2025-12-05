using Trippy.Domain.Entities;

namespace Trippy.Domain.Interfaces
{
    public record NotificationRequest(string Recipient, string Subject, string Body, NotificationChannel Channel = NotificationChannel.Email, DateTimeOffset? ScheduledAt = null);

    public interface INotificationService
    {
        Task QueueNotificationAsync(NotificationRequest request, CancellationToken ct = default);

    }
}
