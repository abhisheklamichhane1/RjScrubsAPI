using Microsoft.AspNetCore.Mvc;
using RjScrubs.Models;
using RjScrubs.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace RjScrubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region CRUD Operations

        // Create a new service
        [HttpPost]
        [Authorize(Roles = "Admin")] // Only admins can create services
        public async Task<IActionResult> CreateService([FromBody] ServiceViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = new Service
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Duration = model.Duration,
                Availability = model.Availability
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetService), new { id = service.Id }, service);
        }

        // Get all services
        [HttpGet]
        public async Task<IActionResult> GetAllServices()
        {
            var services = await _context.Services.ToListAsync();
            return Ok(services);
        }

        // Get a service by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return NotFound();

            return Ok(service);
        }

        // Update a service
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // Only admins can update services
        public async Task<IActionResult> UpdateService(int id, [FromBody] ServiceViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return NotFound();

            service.Name = model.Name;
            service.Description = model.Description;
            service.Price = model.Price;
            service.Duration = model.Duration;
            service.Availability = model.Availability;

            _context.Services.Update(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete a service
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Only admins can delete services
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return NotFound();

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion
    }
}
