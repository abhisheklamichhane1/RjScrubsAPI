using Microsoft.AspNetCore.Mvc;
using RjScrubs.Models;
using RjScrubs.Repositories;
using RjScrubs.ViewModels;


namespace RjScrubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationController(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        // Get all notifications
        [HttpGet]
        public async Task<IActionResult> GetAllNotifications()
        {
            var notifications = await _notificationRepository.GetAllNotificationsAsync();
            return Ok(notifications);
        }

        // Get notification by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificationById(int id)
        {
            var notification = await _notificationRepository.GetNotificationByIdAsync(id);
            if (notification == null)
                return NotFound(new { message = "Notification not found." });

            return Ok(notification);
        }

        // Create a new notification
        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] NotificationViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid notification details.", errors = ModelState });

            var notification = new Notification
            {
                Recipient = model.Recipient,
                NotificationType = model.NotificationType, // Ensure this matches the model's type
                Subject = model.Subject,
                Body = model.Message,
                SentDate = model.SentDateTime,
                Status = model.Status,
                Metadata = model.ReferenceId
            };

            await _notificationRepository.AddNotificationAsync(notification);

            return CreatedAtAction(nameof(GetNotificationById), new { id = notification.Id }, notification);
        }


        // Update an existing notification
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotification(int id, [FromBody] NotificationViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid notification details.", errors = ModelState });

            var notification = await _notificationRepository.GetNotificationByIdAsync(id);
            if (notification == null)
                return NotFound(new { message = "Notification not found." });

            notification.Recipient = model.Recipient;
            notification.NotificationType = model.NotificationType;
            notification.Subject = model.Subject;
            notification.Body = model.Message;
            notification.SentDate = model.SentDateTime;
            notification.Status = model.Status;
            notification.Metadata = model.ReferenceId;

            await _notificationRepository.UpdateNotificationAsync(notification);

            return NoContent();
        }

        // Delete a notification
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var notification = await _notificationRepository.GetNotificationByIdAsync(id);
            if (notification == null)
                return NotFound(new { message = "Notification not found." });

            await _notificationRepository.DeleteNotificationAsync(id);

            return NoContent();
        }

        // Get notifications by status
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetNotificationsByStatus(RjScrubs.Models.NotificationStatus status)
        {
            var notifications = await _notificationRepository.GetNotificationsByStatusAsync(status);
            return Ok(notifications);
        }

        // Get notifications by recipient
        [HttpGet("recipient/{recipient}")]
        public async Task<IActionResult> GetNotificationsByRecipient(string recipient)
        {
            var notifications = await _notificationRepository.GetNotificationsByRecipientAsync(recipient);
            return Ok(notifications);
        }
    }
}
