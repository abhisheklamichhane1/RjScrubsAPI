using System;
using System.ComponentModel.DataAnnotations;
using ViewModelStatus = RjScrubs.ViewModels.NotificationStatus;

namespace RjScrubs.ViewModels
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

    // ViewModel for notification details
    public class NotificationViewModel
    {
        public int Id { get; set; } // Unique identifier for the notification

        [Required]
        [Display(Name = "Recipient")]
        [StringLength(255, ErrorMessage = "Recipient cannot exceed 255 characters.")]
        public string Recipient { get; set; } // The recipient of the notification (email, phone number, etc.)

        [Required]
        [Display(Name = "Message")]
        [StringLength(2000, ErrorMessage = "Message cannot exceed 2000 characters.")]
        public string Message { get; set; } // The content of the notification

        [Required]
        [Display(Name = "Sent Date & Time")]
        [DataType(DataType.DateTime)]
        public DateTime SentDateTime { get; set; } // The date and time when the notification was sent

        [Required]
        [Display(Name = "Notification Type")]
        public string NotificationType { get; set; } // The type of notification (e.g., Email, SMS)

        [Display(Name = "Status")]
        public RjScrubs.Models.NotificationStatus Status { get; set; } // Status of the notification (e.g., Sent, Failed)

        [Display(Name = "Reference Id")]
        [StringLength(100, ErrorMessage = "Reference Id cannot exceed 100 characters.")]
        public string ReferenceId { get; set; } // Optional reference ID, linking to another entity (e.g., booking, payment)

        // Add the Subject property if it's needed
        [Display(Name = "Subject")]
        public string Subject { get; set; } // Subject of the notification
    }
}
