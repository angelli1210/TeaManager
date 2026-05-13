using System.ComponentModel.DataAnnotations;

namespace TeaManager.API.Models.DTO
{
    public class UpdateSupplierRequestDTO
    {
        // BrandId is not included - received from URL, not request body.

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public required string SupplierName { get; set; }

        [Required]
        [StringLength(100)]
        public required string Country { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string ContactEmail { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public required string Phone { get; set; }

    }
}
