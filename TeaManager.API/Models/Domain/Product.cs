using System.ComponentModel.DataAnnotations.Schema;

namespace TeaManager.API.Models.Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int HarvestYear { get; set; }
        public required string Origin { get; set; }
        public DateTime CreatedAt { get; set; }

        //Foreign keys
        public Guid BrandId { get; set; }
        public Guid SupplierId { get; set; }

        //Navigation properties
        public Brand Brand { get; set; } = null!;
        public Supplier Supplier { get; set; } = null!;
        public ICollection<SupplierOrder> SupplierOrders { get; set; } = new List<SupplierOrder>();
    }
}