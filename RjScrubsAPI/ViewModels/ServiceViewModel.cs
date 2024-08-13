using System.ComponentModel.DataAnnotations;

namespace RjScrubs.ViewModels
{
    // ViewModel for service details
    public class ServiceViewModel
    {
        public int Id { get; set; } // Unique identifier for the service

        [Required]
        [StringLength(100, ErrorMessage = "Service name cannot exceed 100 characters.")]
        [Display(Name = "Service Name")]
        public string Name { get; set; } // Name of the service

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Service Description")]
        public string Description { get; set; } // Description of the service

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        public decimal Price { get; set; } // Price of the service

        [Required]
        [Display(Name = "Duration (in minutes)")]
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be at least 1 minute.")]
        public int Duration { get; set; } // Duration of the service in minutes

        [Required]
        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; } // Indicates if the service is available for booking

        [StringLength(50, ErrorMessage = "Category name cannot exceed 50 characters.")]
        [Display(Name = "Category")]
        public string Category { get; set; } // Category to which the service belongs
    }
}
