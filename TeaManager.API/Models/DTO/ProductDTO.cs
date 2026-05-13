namespace TeaManager.API.Models.DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int HarvestYear { get; set; }
        public required string Origin { get; set; }
        public DateTime CreatedAt { get; set; }

        //FK
        public int BrandId { get; set; } // Guid for DB, brand id = int business key
        public required string BrandName { get; set; }

        public int SupplierId { get; set; }
        public required string SupplierName { get; set; }
    }
}