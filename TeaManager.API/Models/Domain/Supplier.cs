namespace TeaManager.API.Models.Domain
{
    public class Supplier
    {
        public Guid Id { get; set; }
        public int SupplierId { get; set; }
        public required string SupplierName { get; set; }
        public required string Country { get; set; }
        public required string ContactEmail { get; set; }
        public required string Phone { get; set; }
        public DateTime CreatedAt { get; set; }

        //Navigation properties
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<SupplierOrder> SupplierOrders { get; set; } = new List<SupplierOrder>();

    }
}