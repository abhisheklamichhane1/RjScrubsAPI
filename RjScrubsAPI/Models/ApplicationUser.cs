using Microsoft.AspNetCore.Identity;
using System;

namespace RjScrubs.Models
{
    // Represents a user in the application
    public class ApplicationUser : IdentityUser
    {
        // Full name of the user
        public string FullName { get; set; }

        // Address of the user
        public string Address { get; set; }

        // Profile picture URL
        public string ProfilePictureUrl { get; set; }

        // Date of birth (nullable if optional)
        public DateTime? DateOfBirth { get; set; }

        // City where the user resides
        public string City { get; set; }

        // State where the user resides
        public string State { get; set; }

        // Zip code for the user's address
        public string ZipCode { get; set; }
    }
}
