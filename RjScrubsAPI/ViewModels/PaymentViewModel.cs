using System;
using System.ComponentModel.DataAnnotations;

namespace RjScrubs.ViewModels
{
    // ViewModel for payment details
    public class PaymentViewModel
    {
        public int Id { get; set; } // Unique identifier for the payment

        [Required]
        [Display(Name = "Booking")]
        public int BookingId { get; set; } // ID of the related booking

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; } // Amount of the payment

        [Required]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; } // Method of payment (e.g., Credit Card, PayPal)

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Payment Date & Time")]
        public DateTime PaymentDateTime { get; set; } // Date and time of the payment

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; } // Additional notes or details about the payment (optional)

        [Display(Name = "Payment Status")]
        public string Status { get; set; } // Status of the payment (e.g., Completed, Pending, Failed)
    }
}
