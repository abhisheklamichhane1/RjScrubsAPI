using Stripe;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using RjScrubs.Models;

namespace RjScrubs.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;

        public PaymentService(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiKey = _configuration["Stripe:ApiKey"];
            StripeConfiguration.ApiKey = _apiKey;
        }

        public async Task<string> CreatePaymentIntentAsync(decimal amount, string currency = "usd", List<string> paymentMethodTypes = null)
        {
            paymentMethodTypes ??= new List<string> { "card" };

            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100),
                Currency = currency,
                PaymentMethodTypes = paymentMethodTypes,
            };

            var service = new PaymentIntentService();
            try
            {
                var paymentIntent = await service.CreateAsync(options);
                return paymentIntent.ClientSecret;
            }
            catch (StripeException ex)
            {
                // Handle exception
                throw new ApplicationException($"Stripe API error: {ex.Message}", ex);
            }
        }

        public async Task<PaymentIntent> GetPaymentIntentAsync(string paymentIntentId)
        {
            var service = new PaymentIntentService();
            try
            {
                return await service.GetAsync(paymentIntentId);
            }
            catch (StripeException ex)
            {
                // Handle exception
                throw new ApplicationException($"Stripe API error: {ex.Message}", ex);
            }
        }

        public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, string paymentMethod, Booking booking)
        {
            // Create a PaymentIntent
            var clientSecret = await CreatePaymentIntentAsync(amount);

            // Mock payment processing result (replace with actual implementation)
            return new PaymentResult
            {
                Success = true,
                TransactionId = "sample-transaction-id" // Replace with actual transaction ID
            };
        }
    }
}
