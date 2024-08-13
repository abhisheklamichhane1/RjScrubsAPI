using Stripe;

namespace RjScrubs.Services
{
    // Service for handling payment processing
    public class PaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;

        public PaymentService(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiKey = _configuration["Stripe:ApiKey"];
            StripeConfiguration.ApiKey = _apiKey;
        }

        public async Task<string> CreatePaymentIntentAsync(decimal amount, string currency = "usd")
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100), // amount in cents
                Currency = currency,
                PaymentMethodTypes = new List<string> { "card" },
            };
            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);
            return paymentIntent.ClientSecret;
        }

        public async Task<PaymentIntent> ConfirmPaymentIntentAsync(string paymentIntentId)
        {
            var service = new PaymentIntentService();
            var paymentIntent = await service.GetAsync(paymentIntentId);
            return paymentIntent;
        }
    }
}
