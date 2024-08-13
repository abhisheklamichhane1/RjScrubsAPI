using System;
using System.ComponentModel.DataAnnotations;

namespace RjScrubs.ViewModels
{
    // ViewModel for report details
    public class ReportViewModel
    {
        public int Id { get; set; } // Unique identifier for the report

        [Required]
        [Display(Name = "Title")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } // The title of the report

        [Required]
        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } // A brief description of the report

        [Display(Name = "Generated On")]
        [DataType(DataType.DateTime)]
        public DateTime GeneratedOn { get; set; } // The date and time when the report was generated

        [Display(Name = "Report Type")]
        [StringLength(50, ErrorMessage = "Report Type cannot exceed 50 characters.")]
        public string ReportType { get; set; } // The type of report (e.g., Summary, Detailed)

        [Display(Name = "Author")]
        [StringLength(100, ErrorMessage = "Author name cannot exceed 100 characters.")]
        public string Author { get; set; } // The author or creator of the report

        [Display(Name = "File Path")]
        [StringLength(500, ErrorMessage = "File Path cannot exceed 500 characters.")]
        public string FilePath { get; set; } // The file path where the report is stored

        [Display(Name = "Status")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string Status { get; set; } // The status of the report (e.g., Draft, Completed, Archived)
    }
}
