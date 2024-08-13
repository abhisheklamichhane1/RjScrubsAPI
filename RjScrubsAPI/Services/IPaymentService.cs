using RjScrubs.Models;
using Stripe;

namespace RjScrubs.Services
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentIntentAsync(decimal amount, string currency = "usd", List<string> paymentMethodTypes = null);
        Task<PaymentIntent> GetPaymentIntentAsync(string paymentIntentId);
        Task<PaymentResult> ProcessPaymentAsync(decimal amount, string paymentMethod, Booking booking);
    }
}
