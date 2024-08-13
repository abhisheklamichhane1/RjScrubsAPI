using System.Text;
using RjScrubs.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RjScrubs.Services
{
    public class ReportService
    {
        // Generates a CSV report of bookings
        public async Task<string> GenerateBookingsReportAsync(IEnumerable<Booking> bookings)
        {
            var csv = new StringBuilder();
            csv.AppendLine("Id,CustomerName,BookingDate,Status");

            foreach (var booking in bookings)
            {
                csv.AppendLine($"{booking.Id},{booking.CustomerName},{booking.BookingDate.ToShortDateString()},{booking.Status}");
            }

            // Save the report to a file or return it as a string
            var filePath = Path.Combine("Reports", $"BookingsReport_{DateTime.Now:yyyyMMddHHmmss}.csv");
            await File.WriteAllTextAsync(filePath, csv.ToString());

            return filePath;
        }

        // Generates a summary report of services
        public async Task<string> GenerateServicesSummaryReportAsync(IEnumerable<Service> services)
        {
            var csv = new StringBuilder();
            csv.AppendLine("Id,ServiceName,Price,Description");

            foreach (var service in services)
            {
                // Ensure these properties are present in the Service model
                csv.AppendLine($"{service.Id},{service.ServiceName},{service.Price},{service.Description}");
            }

            // Save the report to a file or return it as a string
            var filePath = Path.Combine("Reports", $"ServicesSummaryReport_{DateTime.Now:yyyyMMddHHmmss}.csv");
            await File.WriteAllTextAsync(filePath, csv.ToString());

            return filePath;
        }
    }
}
