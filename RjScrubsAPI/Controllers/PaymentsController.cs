using Microsoft.AspNetCore.Mvc;
using RjScrubs.Models;
using RjScrubs.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace RjScrubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPaymentService _paymentService;

        public PaymentsController(ApplicationDbContext context, IPaymentService paymentService)
        {
            _context = context;
            _paymentService = paymentService;
        }

        #region Payment Operations

        // Process a payment
        [HttpPost("process")]
        [Authorize]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var booking = await _context.Bookings
                .Include(b => b.Service)
                .FirstOrDefaultAsync(b => b.Id == model.BookingId);

            if (booking == null)
                return NotFound("Booking not found.");

            var result = await _paymentService.ProcessPaymentAsync(model.Amount, model.PaymentMethod, booking);

            if (!result.Success)
                return BadRequest(result.ErrorMessage);

            booking.Status = BookingStatus.Paid;
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Payment processed successfully." });
        }

        // Retrieve payment details (optional)
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetPayment(int id)
        {
            // This assumes you have a Payment model and related data
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
                return NotFound();

            return Ok(payment);
        }

        #endregion
    }
}
