using System.ComponentModel.DataAnnotations;

namespace TeaManager.API.Models.DTO
{
    public class CreateSupplierRequestDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "BrandId must be greater than 0.")]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Supplier name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Supplier name must be between 2 and 100 characters.")]
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