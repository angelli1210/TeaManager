using System.ComponentModel.DataAnnotations;

namespace TeaManager.API.Models.DTO
{
    public class UpdateBrandRequestDTO
    {
        // BrandId is not included - received from URL, not request body.

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public required string BrandName { get; set; }

        [Required]
        [StringLength(100)]
        public required string Country { get; set; }

        [Required]
        [Range(1500, 2026, ErrorMessage = "Founded year must be between 1500 and 2026.")]
        public int FoundedYear { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be 8-100 characters.")]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#^()_+=\-]).+$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public required string Password { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public required string Phone { get; set; }

        [Required]
        [StringLength(255)]
        public required string Address { get; set; }

        [Required]
        [StringLength(50)]
        public required string BusinessRegNumber { get; set; }

        [Required]
        [StringLength(100)]
        public required string OwnerName { get; set; }
    }
}
