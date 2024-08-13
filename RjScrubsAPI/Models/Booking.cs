using System;
using System.ComponentModel.DataAnnotations;

namespace RjScrubs.Models
{
    // Represents a booking made by a customer
    public class Booking
    {
        // Unique identifier for the booking
        [Key]
        public int Id { get; set; }

        // Foreign key to the service being booked
        [Required]
        public int ServiceId { get; set; }

        // Navigation property for the service
        public Service Service { get; set; }

        // Foreign key to the user who made the booking
        [Required]
        public string UserId { get; set; }

        // Navigation property for the user
        public ApplicationUser User { get; set; }

        // Date and time when the booking is scheduled
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Booking Date & Time")]
        public DateTime BookingDate { get; set; }

        // Status of the booking (e.g., Confirmed, Pending, Canceled)
        [StringLength(50)]
        [Display(Name = "Booking Status")]
        public string Status { get; set; } = "Pending"; // Default value

        // Optional: Additional properties like notes or special requests
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        // Total price for the booking (optional)
        [DataType(DataType.Currency)]
        public decimal? TotalPrice { get; set; }

        public string CustomerName { get; set; } // This property should exist
        public string ServiceName { get; set; } // This should be defined
    }
}
