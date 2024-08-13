using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace RjScrubs.Services
{
    // Service for handling SMS notifications
    public class SmsService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SmsService> _logger; // Logger for diagnostics
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _twilioPhoneNumber;

        public SmsService(IConfiguration configuration, ILogger<SmsService> logger)
        {
            _configuration = configuration;
            _logger = logger; // Initialize logger
            _accountSid = _configuration["SmsSettings:AccountSid"];
            _authToken = _configuration["SmsSettings:AuthToken"];
            _twilioPhoneNumber = _configuration["SmsSettings:TwilioPhoneNumber"];

            // Initialize Twilio client
            TwilioClient.Init(_accountSid, _authToken);
        }

        public async Task SendSmsAsync(string toPhoneNumber, string message)
        {
            try
            {
                var messageOptions = new CreateMessageOptions(new PhoneNumber(toPhoneNumber))
                {
                    From = new PhoneNumber(_twilioPhoneNumber),
                    Body = message
                };

                var messageResource = await MessageResource.CreateAsync(messageOptions);

                _logger.LogInformation($"SMS sent to {toPhoneNumber}. Message SID: {messageResource.Sid}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send SMS to {toPhoneNumber}. Error: {ex.Message}");
                // Optionally, rethrow or handle the exception based on your requirements
            }
        }
    }
}
