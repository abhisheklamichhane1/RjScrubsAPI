namespace RjScrubs.Models
{
    // Represents a booking made by a customer
    public class Booking
    {
        // Unique identifier for the booking
        public int Id { get; set; }

        // Foreign key to the service being booked
        public int ServiceId { get; set; }

        // Navigation property for the service
        public Service Service { get; set; }

        // Foreign key to the user who made the booking
        public string UserId { get; set; }

        // Navigation property for the user
        public ApplicationUser User { get; set; }

        // Date and time when the booking is scheduled
        public DateTime BookingDate { get; set; }

        // Status of the booking (e.g., Confirmed, Pending, Canceled)
        public string Status { get; set; }

        // Optional: Additional properties like notes or special requests
        public string Notes { get; set; }
    }
}
