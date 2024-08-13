using Microsoft.AspNetCore.Mvc;
using RjScrubs.Services;
using RjScrubs.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace RjScrubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;

        public NotificationsController(IEmailService emailService, ISmsService smsService)
        {
            _emailService = emailService;
            _smsService = smsService;
        }

        #region Email Notifications

        // Send an email notification
        [HttpPost("send-email")]
        [Authorize]
        public async Task<IActionResult> SendEmailNotification([FromBody] EmailNotificationViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _emailService.SendEmailAsync(model.ToAddress, model.Subject, model.Body);

            if (!result.Success)
                return BadRequest(result.ErrorMessage);

            return Ok(new { message = "Email sent successfully." });
        }

        #endregion

        #region SMS Notifications

        // Send an SMS notification
        [HttpPost("send-sms")]
        [Authorize]
        public async Task<IActionResult> SendSmsNotification([FromBody] SmsNotificationViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _smsService.SendSmsAsync(model.ToNumber, model.Message);

            if (!result.Success)
                return BadRequest(result.ErrorMessage);

            return Ok(new { message = "SMS sent successfully." });
        }

        #endregion
    }
}
