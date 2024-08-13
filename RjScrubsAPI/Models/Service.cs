namespace RjScrubs.Models
{
    // Represents a service offered by the application
    public class Service
    {
        // Unique identifier for the service
        public int Id { get; set; }

        // Name of the service
        public string Name { get; set; }

        // Description of the service
        public string Description { get; set; }

        // Price of the service
        public decimal Price { get; set; }

        // Duration of the service in minutes
        public int Duration { get; set; }

        // Indicates if the service is available
        public bool IsAvailable { get; set; }

        // Category to which the service belongs
        public string Category { get; set; }

        // Optional: Add other properties as needed
        public bool Availability { get; set; } // Ensure this property exists

        // Navigation property for bookings (if needed)
        public ICollection<Booking> Bookings { get; set; }
    }
}
