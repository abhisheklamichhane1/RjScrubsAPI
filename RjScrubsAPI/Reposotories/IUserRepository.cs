using RjScrubs.Models;

namespace RjScrubs.Reposotories
{
    public interface IUserRepository : IRepository<User>
    {
        // Retrieve users by a specific role
        Task<IEnumerable<User>> GetUsersByRoleAsync(string role);

        // Find a user by email
        Task<User> GetUserByEmailAsync(string email);

        // Check if a user exists by email
        Task<bool> UserExistsByEmailAsync(string email);

        // Other user-specific methods can be added here
    }
}
