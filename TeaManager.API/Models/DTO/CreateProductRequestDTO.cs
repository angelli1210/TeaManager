using System.ComponentModel.DataAnnotations;

namespace TeaManager.API.Models.DTO
{
    public class CreateProductRequestDTO
    {
        [Required, StringLength(200, MinimumLength = 2)]
        public required string ProductName { get; set; }

        [Required, StringLength(1000)]
        public required string Description { get; set; }

        [Required, Range(0.01, 99999.99)]
        public decimal Price { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [Required, Range(2015, 2026)]
        public int HarvestYear { get; set; }

        [Required, StringLength(200)]
        public required string Origin { get; set; }

        //FK only can receive integer not string
        [Required, Range(1, int.MaxValue)]
        public int BrandId { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int SupplierId { get; set; }
    }
}