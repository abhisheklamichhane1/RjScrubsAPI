using RjScrubs.Models;
using RjScrubs.ViewModels;

namespace RjScrubs.Repositories
{
    public interface IServiceRepository
    {
        // Get all services
        Task<IEnumerable<Service>> GetAllServicesAsync();

        // Get a service by ID
        Task<Service> GetServiceByIdAsync(int id);

        // Create a new service
        Task<Service> CreateServiceAsync(ServiceViewModel model);

        // Update an existing service
        Task<Service> UpdateServiceAsync(int id, ServiceViewModel model);

        // Delete a service
        Task<bool> DeleteServiceAsync(int id);
    }
}
