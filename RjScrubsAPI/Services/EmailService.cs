using System.Net;
using System.Net.Mail;

namespace RjScrubs.Services
{
    // Service for handling email notifications
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger; // Logger for diagnostics
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger; // Initialize logger
            _smtpServer = _configuration["EmailSettings:SmtpServer"];
            _smtpPort = int.TryParse(_configuration["EmailSettings:SmtpPort"], out var port) ? port : 587; // Default port if parsing fails
            _smtpUser = _configuration["EmailSettings:SmtpUser"];
            _smtpPass = _configuration["EmailSettings:SmtpPass"];
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpUser),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(toEmail);

            try
            {
                using (var client = new SmtpClient(_smtpServer, _smtpPort))
                {
                    client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
                    client.EnableSsl = true;

                    await client.SendMailAsync(mailMessage);

                    _logger.LogInformation($"Email sent to {toEmail} with subject '{subject}'.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send email to {toEmail}. Subject: '{subject}'. Error: {ex.Message}");
                // Optionally, rethrow or handle the exception based on your requirements
            }
        }
    }
}
