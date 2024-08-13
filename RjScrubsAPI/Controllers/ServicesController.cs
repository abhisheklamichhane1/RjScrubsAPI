using Microsoft.AspNetCore.Mvc;
using RjScrubs.Models;
using RjScrubs.ViewModels;
using RjScrubs.Repositories; // Make sure to include the namespace for the repository
using Microsoft.AspNetCore.Authorization;

namespace RjScrubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceRepository _serviceRepository;

        public ServicesController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        #region CRUD Operations

        // Create a new service
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateService([FromBody] ServiceViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = await _serviceRepository.CreateServiceAsync(model);

            return CreatedAtAction(nameof(GetService), new { id = service.Id }, service);
        }

        // Get all services
        [HttpGet]
        public async Task<IActionResult> GetAllServices()
        {
            var services = await _serviceRepository.GetAllServicesAsync();

            if (services == null || !services.Any())
                return NotFound("No services found.");

            return Ok(services);
        }

        // Get a service by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetService(int id)
        {
            var service = await _serviceRepository.GetServiceByIdAsync(id);

            if (service == null)
                return NotFound("Service not found.");

            return Ok(service);
        }

        // Update a service
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateService(int id, [FromBody] ServiceViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = await _serviceRepository.UpdateServiceAsync(id, model);

            if (service == null)
                return NotFound("Service not found.");

            return NoContent();
        }

        // Delete a service
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var result = await _serviceRepository.DeleteServiceAsync(id);

            if (!result)
                return NotFound("Service not found.");

            return NoContent();
        }

        #endregion
    }
}
