using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RjScrubs.Models;
using RjScrubs.ViewModels;
using RjScrubs.Repositories;

namespace RjScrubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingsController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        #region Booking Operations

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBooking([FromBody] BookingViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = await _bookingRepository.GetBookingByIdAsync(model.ServiceId);
            if (service == null)
                return NotFound("Service not found.");

            var booking = new Booking
            {
                ServiceId = model.ServiceId,
                UserId = model.UserId,
                BookingDate = model.BookingDate,
                Status = model.Status,
                Notes = model.Notes,
                TotalPrice = model.TotalPrice
            };

            await _bookingRepository.AddBookingAsync(booking);
            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
        }

        [HttpGet("user/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUserBookings(string userId)
        {
            var bookings = await _bookingRepository.GetBookingsByUserIdAsync(userId);
            if (bookings == null || !bookings.Any())
                return NotFound("No bookings found for this user.");

            return Ok(bookings);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetBooking(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            if (booking == null)
                return NotFound("Booking not found.");

            return Ok(booking);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            if (booking == null)
                return NotFound("Booking not found.");

            var service = await _bookingRepository.IsServiceAvailableAsync(model.ServiceId, model.BookingDate);
            if (!service)
                return BadRequest("Service is not available at the selected time.");

            booking.ServiceId = model.ServiceId;
            booking.BookingDate = model.BookingDate;
            booking.Status = model.Status;
            booking.Notes = model.Notes;
            booking.TotalPrice = model.TotalPrice;

            await _bookingRepository.UpdateBookingAsync(booking);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> CancelBooking(int id)
        {
            await _bookingRepository.DeleteBookingAsync(id);
            return NoContent();
        }

        #endregion
    }
}
