using System.ComponentModel.DataAnnotations;

namespace RjScrubs.Models
{
    // Represents a service offered by the application
    public class Service
    {
        // Unique identifier for the service
        [Key]
        public int Id { get; set; }

        // Name of the service
        [Required]
        [StringLength(100, ErrorMessage = "Service name cannot exceed 100 characters.")]
        public string Name { get; set; }

        // Description of the service
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        // Price of the service
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        // Duration of the service in minutes
        [Required]
        [Display(Name = "Duration (in minutes)")]
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be at least 1 minute.")]
        public int Duration { get; set; }

        // Indicates if the service is available
        [Required]
        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }

        // Category to which the service belongs
        [StringLength(50, ErrorMessage = "Category name cannot exceed 50 characters.")]
        public string Category { get; set; }

        // Navigation property for bookings
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
