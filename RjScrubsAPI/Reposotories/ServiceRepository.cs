using Microsoft.EntityFrameworkCore;
using RjScrubs.Data;
using RjScrubs.Models;
using RjScrubs.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RjScrubs.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<Service> GetServiceByIdAsync(int id)
        {
            return await _context.Services.FindAsync(id);
        }

        public async Task<Service> CreateServiceAsync(ServiceViewModel model)
        {
            var service = new Service
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Duration = model.Duration,
                IsAvailable = model.IsAvailable
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return service;
        }

        public async Task<Service> UpdateServiceAsync(int id, ServiceViewModel model)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null) return null;

            service.Name = model.Name;
            service.Description = model.Description;
            service.Price = model.Price;
            service.Duration = model.Duration;
            service.IsAvailable = model.IsAvailable;

            _context.Services.Update(service);
            await _context.SaveChangesAsync();
            return service;
        }

        public async Task<bool> DeleteServiceAsync(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null) return false;

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
