using System;
using System.ComponentModel.DataAnnotations;

namespace RjScrubs.ViewModels
{
    // ViewModel for booking details
    public class BookingViewModel
    {
        public int Id { get; set; } // Unique identifier for the booking

        [Required]
        [Display(Name = "Service")]
        public int ServiceId { get; set; } // ID of the service being booked

        [Required]
        [Display(Name = "User")]
        public string UserId { get; set; } // ID of the user making the booking

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Booking Date & Time")]
        public DateTime BookingDateTime { get; internal set; } // Date and time of the booking

        [Required]
        [Display(Name = "Booking Status")]
        public string Status { get; set; } // Status of the booking (e.g., Pending, Confirmed, Completed, Cancelled)

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; } // Additional notes or special requests

        [DataType(DataType.Currency)]
        public decimal? TotalPrice { get; set; } // Total price for the booking (optional)
        public DateTime BookingDate { get; internal set; }
    }
}
