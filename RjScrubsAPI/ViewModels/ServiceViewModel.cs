using System.ComponentModel.DataAnnotations;

namespace RjScrubs.ViewModels
{
    // ViewModel for service details
    public class ServiceViewModel
    {
        public int Id { get; set; } // Unique identifier for the service

        [Required]
        [StringLength(100, ErrorMessage = "Service name cannot exceed 100 characters.")]
        public string Name { get; set; } // Name of the service

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } // Description of the service

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; } // Price of the service

        [Required]
        [Display(Name = "Duration (in minutes)")]
        public int Duration { get; set; } // Duration of the service in minutes

        public bool Availability { get; set; } // Add this property
        public bool IsActive { get; set; } // Indicates if the service is currently active
    }
}
