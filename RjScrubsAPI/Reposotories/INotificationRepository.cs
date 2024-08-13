using System.Collections.Generic;
using System.Threading.Tasks;
using RjScrubs.Models;

namespace RjScrubs.Repositories
{
    public interface INotificationRepository
    {
        Task AddNotificationAsync(Notification notification);
        Task<Notification> GetNotificationByIdAsync(int id);
        Task<IEnumerable<Notification>> GetAllNotificationsAsync();
        Task UpdateNotificationAsync(Notification notification);
        Task DeleteNotificationAsync(int id);
        Task<IEnumerable<Notification>> GetNotificationsByStatusAsync(NotificationStatus status); // Updated
        Task<IEnumerable<Notification>> GetNotificationsByRecipientAsync(string recipient);
    }
}
