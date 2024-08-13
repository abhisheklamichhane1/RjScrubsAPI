using System.Collections.Generic;
using System.Threading.Tasks;
using RjScrubs.Models;

namespace RjScrubs.Repositories
{
    public interface IReportRepository
    {
        // Adds a new report to the repository
        Task AddReportAsync(Report report);

        // Updates an existing report in the repository
        Task UpdateReportAsync(Report report);

        // Deletes a report from the repository by its ID
        Task DeleteReportAsync(int id);

        // Retrieves a report by its ID
        Task<Report> GetReportByIdAsync(int id);

        // Retrieves all reports from the repository
        Task<IEnumerable<Report>> GetAllReportsAsync();

        // Retrieves reports by type
        Task<IEnumerable<Report>> GetReportsByTypeAsync(string reportType);

        Task<bool> ReportExistsAsync(int id);

        // Retrieves reports based on a search query or other criteria
        Task<IEnumerable<Report>> SearchReportsAsync(string query);
    }
}
