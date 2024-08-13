using Microsoft.AspNetCore.Mvc;
using RjScrubs.Services;
using RjScrubs.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace RjScrubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        #region Reporting

        // Get a report on bookings
        [HttpGet("bookings")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetBookingsReport([FromQuery] ReportFilterViewModel filter)
        {
            var report = await _reportService.GetBookingsReportAsync(filter);

            if (report == null)
                return NotFound("No report data found.");

            return Ok(report);
        }

        // Get a report on revenue
        [HttpGet("revenue")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRevenueReport([FromQuery] ReportFilterViewModel filter)
        {
            var report = await _reportService.GetRevenueReportAsync(filter);

            if (report == null)
                return NotFound("No report data found.");

            return Ok(report);
        }

        #endregion
    }
}
