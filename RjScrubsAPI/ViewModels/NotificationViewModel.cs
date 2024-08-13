using System;
using System.ComponentModel.DataAnnotations;

namespace RjScrubs.ViewModels
{
    // ViewModel for notification details
    public class NotificationViewModel
    {
        public int Id { get; set; } // Unique identifier for the notification

        [Required]
        [Display(Name = "Recipient")]
        public string Recipient { get; set; } // The recipient of the notification (email, phone number, etc.)

        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; } // The content of the notification

        [Required]
        [Display(Name = "Sent Date & Time")]
        [DataType(DataType.DateTime)]
        public DateTime SentDateTime { get; set; } // The date and time when the notification was sent

        [Required]
        [Display(Name = "Notification Type")]
        public string NotificationType { get; set; } // The type of notification (e.g., Email, SMS)

        [Display(Name = "Status")]
        public string Status { get; set; } // Status of the notification (e.g., Sent, Failed)

        [Display(Name = "Reference Id")]
        public string ReferenceId { get; set; } // Optional reference ID, linking to another entity (e.g., booking, payment)
    }
}
