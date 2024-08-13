using System.ComponentModel.DataAnnotations;

namespace RjScrubs.ViewModels
{
    // ViewModel for payment details
    public class PaymentViewModel
    {
        // Unique identifier for the payment
        public int Id { get; set; }

        // ID of the related booking
        [Required]
        [Display(Name = "Booking")]
        public int BookingId { get; set; }

        // Amount of the payment
        [Required]
        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        // Method of payment (e.g., Credit Card, PayPal)
        [Required]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }

        // Date and time of the payment
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Payment Date & Time")]
        public DateTime PaymentDateTime { get; set; }

        // Additional notes or details about the payment (optional)
        [StringLength(500, ErrorMessage = "Notes can't exceed 500 characters.")]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        // Status of the payment (e.g., Completed, Pending, Failed)
        [Required]
        [Display(Name = "Payment Status")]
        public string Status { get; set; }
    }
}
