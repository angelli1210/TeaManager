namespace TeaManager.API.Models.Domain
{
    public class SupplierOrder
    {
        public Guid Id { get; set; }
        public int SupplierOrderId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Remark { get; set; }//Nullable
        public DateTime CreatedAt { get; set; }

        //Foreign keys
        public Guid SupplierId { get; set; }
        public Guid ProductId { get; set; }

        //Navigation properties
        public Supplier Supplier { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}