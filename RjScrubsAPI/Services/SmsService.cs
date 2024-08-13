using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Microsoft.Extensions.Configuration;

namespace RjScrubs.Services
{
    // Service for handling SMS notifications
    public class SmsService
    {
        private readonly IConfiguration _configuration;
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _twilioPhoneNumber;

        public SmsService(IConfiguration configuration)
        {
            _configuration = configuration;
            _accountSid = _configuration["SmsSettings:AccountSid"];
            _authToken = _configuration["SmsSettings:AuthToken"];
            _twilioPhoneNumber = _configuration["SmsSettings:TwilioPhoneNumber"];

            // Initialize Twilio client
            TwilioClient.Init(_accountSid, _authToken);
        }

        public async Task SendSmsAsync(string toPhoneNumber, string message)
        {
            var messageOptions = new CreateMessageOptions(new PhoneNumber(toPhoneNumber))
            {
                From = new PhoneNumber(_twilioPhoneNumber),
                Body = message
            };

            var messageResource = await MessageResource.CreateAsync(messageOptions);
        }
    }
}
