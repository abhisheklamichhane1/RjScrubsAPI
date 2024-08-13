namespace RjScrubs.Models
{
    // Represents a notification sent to a user
    public class Notification
    {
        // Unique identifier for the notification
        public int Id { get; set; }

        // Recipient of the notification (e.g., user ID or email address)
        public string Recipient { get; set; }

        // Type of the notification (e.g., Email, SMS)
        public string NotificationType { get; set; }

        // Subject of the notification (e.g., Booking Confirmation)
        public string Subject { get; set; }

        // Body content of the notification
        public string Body { get; set; }

        // Date and time when the notification was sent
        public DateTime SentDate { get; set; }

        // Status of the notification (e.g., Sent, Failed)
        public string Status { get; set; }

        // Optional: Additional metadata or information
        public string Metadata { get; set; }
    }
}
