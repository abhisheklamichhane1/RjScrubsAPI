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
        public string Title { get; set; } // The title of the report

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; } // A brief description of the report

        [Display(Name = "Generated On")]
        [DataType(DataType.DateTime)]
        public DateTime GeneratedOn { get; set; } // The date and time when the report was generated

        [Display(Name = "Report Type")]
        public string ReportType { get; set; } // The type of report (e.g., Summary, Detailed)

        [Display(Name = "Author")]
        public string Author { get; set; } // The author or creator of the report

        [Display(Name = "File Path")]
        public string FilePath { get; set; } // The file path where the report is stored

        [Display(Name = "Status")]
        public string Status { get; set; } // The status of the report (e.g., Draft, Completed, Archived)
    }
}
