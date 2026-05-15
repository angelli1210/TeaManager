using System.ComponentModel.DataAnnotations;

namespace TeaManager.API.Models.DTO
{
    public class CreateBrandRequestDTO
    {
        [Required(ErrorMessage = "Brand name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Brand name must be between 2 and 100 characters.")]
        public required string BrandName { get; set; }

        [Required]
        [StringLength(100)]
        public required string Country { get; set; }

        [Required]
        [Range(1800, 2026, ErrorMessage = "Founded year must be between 1800 and 2026.")]
        public int FoundedYear { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }

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
        // CreatedAt is excluded - set automatically in the controller.
    }
}
