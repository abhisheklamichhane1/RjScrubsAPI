using Microsoft.EntityFrameworkCore;
using RjScrubs.Models;

namespace RjScrubs.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly DbContext _context;

        public ReportRepository(DbContext context)
        {
            _context = context;
        }

        public async Task AddReportAsync(Report report)
        {
            _context.Set<Report>().Add(report);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReportAsync(Report report)
        {
            _context.Set<Report>().Update(report);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReportAsync(int id)
        {
            var report = await _context.Set<Report>().FindAsync(id);
            if (report != null)
            {
                _context.Set<Report>().Remove(report);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Report> GetReportByIdAsync(int id)
        {
            return await _context.Set<Report>().FindAsync(id);
        }

        public async Task<IEnumerable<Report>> GetAllReportsAsync()
        {
            return await _context.Set<Report>().ToListAsync();
        }

        public async Task<IEnumerable<Report>> GetReportsByTypeAsync(string reportType)
        {
            return await _context.Set<Report>()
                .Where(r => r.ReportType == reportType)
                .ToListAsync();
        }

        public async Task<IEnumerable<Report>> SearchReportsAsync(string query)
        {
            return await _context.Set<Report>()
                .Where(r => r.Title.Contains(query) || r.Description.Contains(query))
                .ToListAsync();
        }

        // Implementation of ReportExistsAsync
        public async Task<bool> ReportExistsAsync(int id)
        {
            return await _context.Set<Report>().AnyAsync(r => r.Id == id);
        }

    }
}
