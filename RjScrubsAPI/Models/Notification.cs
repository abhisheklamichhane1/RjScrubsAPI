using System;
using System.ComponentModel.DataAnnotations;
using ModelStatus = RjScrubs.Models.NotificationStatus;

namespace RjScrubs.Models
{
    // Enum for notification types
    public enum NotificationType
    {
        Email,
        SMS,
        PushNotification
    }

    // Enum for notification status
    public enum NotificationStatus
    {
        Sent,
        Failed,
        Pending
    }

    // Represents a notification sent to a user
    public class Notification
    {
        // Unique identifier for the notification
        public int Id { get; set; }

        // Recipient of the notification (e.g., user ID or email address)
        [Required]
        [StringLength(255, ErrorMessage = "Recipient cannot exceed 255 characters.")]
        public string Recipient { get; set; }

        // Type of the notification (e.g., Email, SMS)
        [Required]
        public string NotificationType { get; set; }

        // Subject of the notification (e.g., Booking Confirmation)
        [StringLength(255, ErrorMessage = "Subject cannot exceed 255 characters.")]
        public string Subject { get; set; }

        // Body content of the notification
        [Required]
        [StringLength(2000, ErrorMessage = "Body cannot exceed 2000 characters.")]
        public string Body { get; set; }

        // Date and time when the notification was sent
        [Required]
        public DateTime SentDate { get; set; }

        // Status of the notification (e.g., Sent, Failed)
        public NotificationStatus Status { get; set; }

        // Optional: Additional metadata or information
        [StringLength(500, ErrorMessage = "Metadata cannot exceed 500 characters.")]
        public string Metadata { get; set; }
    }
}
