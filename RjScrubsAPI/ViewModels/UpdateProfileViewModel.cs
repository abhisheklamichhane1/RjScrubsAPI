using System;
using System.ComponentModel.DataAnnotations;

namespace RjScrubs.ViewModels
{
    // ViewModel for updating user profile
    public class UpdateProfileViewModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [StringLength(200)]
        [Display(Name = "Profile Picture URL")]
        public string ProfilePictureUrl { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(100)]
        [Display(Name = "City")]
        public string City { get; set; }

        [StringLength(50)]
        [Display(Name = "State")]
        public string State { get; set; }

        [StringLength(10)]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
    }
}
