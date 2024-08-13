using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RjScrubs.Models;
using RjScrubs.ViewModels;
using Microsoft.EntityFrameworkCore;
using RjScrubs.Data;

namespace RjScrubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // Ensure only admins can access these endpoints
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Service Management

        // Create a new service
        [HttpPost("services")]
        public async Task<IActionResult> CreateService([FromBody] ServiceViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = new Service
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Duration = model.Duration,
                IsAvailable = model.IsAvailable // Map Availability to IsAvailable
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetService), new { id = service.Id }, service);
        }

        // Get a service by ID
        [HttpGet("services/{id}")]
        public async Task<IActionResult> GetService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return NotFound();

            return Ok(service);
        }

        // Update a service
        [HttpPut("services/{id}")]
        public async Task<IActionResult> UpdateService(int id, [FromBody] ServiceViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return NotFound();

            service.Name = model.Name;
            service.Description = model.Description;
            service.Price = model.Price;
            service.Duration = model.Duration;
            service.IsAvailable = model.IsAvailable; // Map Availability to IsAvailable

            _context.Services.Update(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete a service
        [HttpDelete("services/{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return NotFound();

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region Booking Management

        // Get all bookings
        [HttpGet("bookings")]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Service) // Optionally include related data
                .Include(b => b.User)
                .ToListAsync();
            return Ok(bookings);
        }

        // Get a booking by ID
        [HttpGet("bookings/{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Service) // Optionally include related data
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        // Approve a booking
        [HttpPost("bookings/{id}/approve")]
        public async Task<IActionResult> ApproveBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                return NotFound();

            booking.Status = "Approved";
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Cancel a booking
        [HttpPost("bookings/{id}/cancel")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                return NotFound();

            booking.Status = "Cancelled";
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion
    }
}
