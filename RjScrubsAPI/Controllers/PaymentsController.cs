using Microsoft.AspNetCore.Mvc;
using RjScrubs.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using RjScrubs.Data;
using RjScrubs.Repositories;
using RjScrubs.Models;
using RjScrubs.Services;


namespace RjScrubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPaymentService _paymentService; // Service for processing payments
        private readonly IPaymentRepository _paymentRepository; // Repository for database operations

        public PaymentsController(ApplicationDbContext context, IPaymentService paymentService, IPaymentRepository paymentRepository)
        {
            _context = context;
            _paymentService = paymentService;
            _paymentRepository = paymentRepository;
        }

        #region Payment Operations

        // Process a payment
        [HttpPost("process")]
        [Authorize] // Any authenticated user can process a payment
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid payment details.", errors = ModelState });

            var booking = await _context.Bookings
                .Include(b => b.Service)
                .FirstOrDefaultAsync(b => b.Id == model.BookingId);

            if (booking == null)
                return NotFound(new { message = "Booking not found." });

            var result = await _paymentService.ProcessPaymentAsync(model.Amount, model.PaymentMethod, booking);

            if (!result.Success)
                return BadRequest(new { message = result.ErrorMessage });

            var payment = new Payment
            {
                BookingId = booking.Id,
                Amount = model.Amount,
                PaymentMethod = model.PaymentMethod,
                PaymentDate = DateTime.UtcNow, // Use UTC for consistency
                Status = "Completed", // Set status to "Completed" upon successful payment
                TransactionId = result.TransactionId, // Store the transaction ID if available
                Notes = model.Notes
            };

            await _paymentRepository.AddPaymentAsync(payment);

            booking.Status = BookingStatus.Paid.ToString();
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Payment processed successfully.",
                paymentId = payment.Id
            });
        }


        // Retrieve payment details
        [HttpGet("{id}")]
        [Authorize] // Any authenticated user can retrieve payment details
        public async Task<IActionResult> GetPayment(int id)
        {
            var payment = await _paymentRepository.GetPaymentByIdAsync(id);

            if (payment == null)
                return NotFound(new { message = "Payment not found." });

            return Ok(payment);
        }

        #endregion
    }
}
