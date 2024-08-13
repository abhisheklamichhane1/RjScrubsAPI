namespace RjScrubs.Models
{
    // Represents a payment associated with a booking
    public class Payment
    {
        // Unique identifier for the payment
        public int Id { get; set; }

        // Foreign key to the booking associated with the payment
        public int BookingId { get; set; }

        // Navigation property for the booking
        public Booking Booking { get; set; }

        // Amount of the payment
        public decimal Amount { get; set; }

        // Payment method (e.g., Credit Card, PayPal)
        public string PaymentMethod { get; set; }

        // Date and time when the payment was made
        public DateTime PaymentDate { get; set; }

        // Status of the payment (e.g., Completed, Pending, Failed)
        public string Status { get; set; }

        // Transaction ID from the payment gateway
        public string TransactionId { get; set; }

        // Additional properties for payment-related details
        public string Notes { get; set; }
    }
}
