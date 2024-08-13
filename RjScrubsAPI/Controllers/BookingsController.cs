using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using RjScrubs.Data;
using RjScrubs.Models;
using RjScrubs.ViewModels;

namespace RjScrubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Booking Operations

        // Create a new booking
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBooking([FromBody] BookingViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = await _context.Services.FindAsync(model.ServiceId);
            if (service == null)
                return NotFound("Service not found.");

            // Optionally check if the user exists and is authenticated

            var booking = new Booking
            {
                ServiceId = model.ServiceId,
                UserId = model.UserId,
                BookingDate = model.BookingDate,
                Status = model.Status,
                Notes = model.Notes,
                TotalPrice = model.TotalPrice
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
        }

        // Get all bookings for a user
        [HttpGet("user/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUserBookings(string userId)
        {
            var bookings = await _context.Bookings
                .Include(b => b.Service)
                .Where(b => b.UserId == userId)
                .ToListAsync();

            if (bookings == null || !bookings.Any())
                return NotFound("No bookings found for this user.");

            return Ok(bookings);
        }

        // Get a booking by ID
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetBooking(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Service)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
                return NotFound("Booking not found.");

            return Ok(booking);
        }

        // Update a booking (e.g., reschedule)
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                return NotFound("Booking not found.");

            var service = await _context.Services.FindAsync(model.ServiceId);
            if (service == null || !IsAvailable(service, model.BookingDate))
                return BadRequest("Service is not available at the selected time.");

            booking.ServiceId = model.ServiceId;
            booking.BookingDate = model.BookingDate;
            booking.Status = model.Status;
            booking.Notes = model.Notes;
            booking.TotalPrice = model.TotalPrice;

            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Cancel a booking
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                return NotFound("Booking not found.");

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region Helper Methods

        private bool IsAvailable(Service service, DateTime bookingDate)
        {
            // Implement actual availability check logic here
            return !_context.Bookings.Any(b => b.ServiceId == service.Id && b.BookingDate == bookingDate);
        }

        #endregion
    }
}
