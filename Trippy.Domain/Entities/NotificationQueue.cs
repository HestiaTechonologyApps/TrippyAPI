using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trippy.Domain.Entities
{
    public enum NotificationChannel { Email = 0, Sms = 1, Push = 2 }
    public enum NotificationStatus { Pending = 0, Processing = 1, Sent = 2, Failed = 3 }
    public class NotificationQueue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationQueueId { get; set; } 
        public string Recipient { get; set; } = default!;
        public string? Subject { get; set; }
        public string Body { get; set; } = default!;
        public NotificationChannel Channel { get; set; } = NotificationChannel.Email;

        public NotificationStatus Status { get; set; } = NotificationStatus.Pending;
        public int RetryCount { get; set; } = 0;
        public DateTimeOffset? ScheduledAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset? ProcessingStartedAt { get; set; }
        public Guid? ProcessingWorkerId { get; set; }

        public string? FailureReason { get; set; }
    }

    public class DeadLetterNotification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeadLetterNotificationId { get; set; } 
        public int NotificationQueueId { get; set; }
        public string Recipient { get; set; } = default!;
        public string? Subject { get; set; }
        public string Body { get; set; } = default!;
        public int RetryCount { get; set; }
        public string? FailureReason { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
