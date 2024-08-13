﻿using Microsoft.EntityFrameworkCore;
using RjScrubs.Data;
using RjScrubs.Models;

namespace RjScrubs.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddNotificationAsync(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<Notification> GetNotificationByIdAsync(int id)
        {
            return await _context.Notifications.FindAsync(id);
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync()
        {
            return await _context.Notifications.ToListAsync();
        }

        public async Task UpdateNotificationAsync(Notification notification)
        {
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNotificationAsync(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByStatusAsync(NotificationStatus status)
        {
            return await _context.Notifications
                .Where(n => n.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByRecipientAsync(string recipient)
        {
            return await _context.Notifications
                .Where(n => n.Recipient == recipient)
                .ToListAsync();
        }
    }
}
