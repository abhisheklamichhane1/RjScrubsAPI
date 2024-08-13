using Microsoft.AspNetCore.Mvc;
using RjScrubs.Models;
using RjScrubs.Repositories;
using RjScrubs.ViewModels;

namespace RjScrubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;

        public ReportsController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        // GET: api/Reports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportViewModel>>> GetReports()
        {
            var reports = await _reportRepository.GetAllReportsAsync();
            var reportViewModels = reports.Select(r => new ReportViewModel
            {
                Id = r.Id,
                Title = r.Title,
                Description = r.Description,
                GeneratedOn = r.CreatedDate,
                ReportType = r.ReportType,
                FilePath = r.FilePath,
                Status = r.Metadata // Ensure this correctly represents the status
            });

            return Ok(reportViewModels);
        }

        // GET: api/Reports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportViewModel>> GetReport(int id)
        {
            var report = await _reportRepository.GetReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            var reportViewModel = new ReportViewModel
            {
                Id = report.Id,
                Title = report.Title,
                Description = report.Description,
                GeneratedOn = report.CreatedDate,
                ReportType = report.ReportType,
                FilePath = report.FilePath,
                Status = report.Metadata // Ensure this correctly represents the status
            };

            return Ok(reportViewModel);
        }

        // POST: api/Reports
        [HttpPost]
        public async Task<ActionResult<Report>> CreateReport(ReportViewModel reportViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var report = new Report
            {
                Title = reportViewModel.Title,
                Description = reportViewModel.Description,
                CreatedDate = reportViewModel.GeneratedOn,
                ReportType = reportViewModel.ReportType,
                Content = "", // Content might be set differently
                FilePath = reportViewModel.FilePath,
                Metadata = reportViewModel.Status // Ensure this correctly represents the status
            };

            await _reportRepository.AddReportAsync(report);

            return CreatedAtAction(nameof(GetReport), new { id = report.Id }, report);
        }

        // PUT: api/Reports/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport(int id, ReportViewModel reportViewModel)
        {
            if (id != reportViewModel.Id)
            {
                return BadRequest();
            }

            var report = new Report
            {
                Id = reportViewModel.Id,
                Title = reportViewModel.Title,
                Description = reportViewModel.Description,
                CreatedDate = reportViewModel.GeneratedOn,
                ReportType = reportViewModel.ReportType,
                Content = "", // Content might be set differently
                FilePath = reportViewModel.FilePath,
                Metadata = reportViewModel.Status // Ensure this correctly represents the status
            };

            try
            {
                await _reportRepository.UpdateReportAsync(report);
            }
            catch (Exception ex)
            {
                // Handle potential errors
                if (!await _reportRepository.ReportExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Reports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            var report = await _reportRepository.GetReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            await _reportRepository.DeleteReportAsync(id);

            return NoContent();
        }
    }
}
