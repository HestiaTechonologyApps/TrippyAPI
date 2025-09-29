namespace Trippy.Domain.DTO
{
    public class AppNotificationDTO
    {
       
        public int AppNotificationId { get; set; }
        public String NotificationType { get; set; } = "";
        public String NotificationTitle { get; set; } = "";
        public String NotificationImage { get; set; } = "";
        public bool IsActive { get; set; } = true;

        public string NotificationLink { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



    }
}
