using System.ComponentModel.DataAnnotations;

namespace TeaManager.API.Models.DTO
{
    public class CreateSupplierOrderRequestDTO
    {
        [Required, Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [StringLength(500)]
        public string? Remark { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int SupplierId { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int ProductId { get; set; }

    }
}