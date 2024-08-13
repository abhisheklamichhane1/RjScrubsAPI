using Microsoft.AspNetCore.Identity;

namespace RjScrubs.Models
{
    // Represents a user in the application
    public class ApplicationUser : IdentityUser
    {
        // Full name of the user
        public string FullName { get; set; }

        // Address of the user
        public string Address { get; set; }

        // Additional information (e.g., phone number, profile picture URL, etc.)
        public string ProfilePictureUrl { get; set; }

        // Date of birth
        public DateTime DateOfBirth { get; set; }

        // Optional: Add other properties as needed
        // Additional properties for profile update
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
