namespace RjScrubs.Models
{
    // Represents a report generated within the system
    public class Report
    {
        // Unique identifier for the report
        public int Id { get; set; }

        // Title or name of the report
        public string Title { get; set; }

        // Description or summary of the report's content
        public string Description { get; set; }

        // The date and time when the report was created
        public DateTime CreatedDate { get; set; }

        // The type or category of the report (e.g., Booking Report, Revenue Report)
        public string ReportType { get; set; }

        // The content or data of the report
        public string Content { get; set; }

        // Optional: Path or URL to the generated report file (e.g., PDF, Excel)
        public string FilePath { get; set; }

        // Optional: Metadata or additional information related to the report
        public string Metadata { get; set; }
    }
}
