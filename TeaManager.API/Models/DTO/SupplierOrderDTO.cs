namespace TeaManager.API.Models.DTO;

public class SupplierOrderDTO
{
        public Guid Id { get; set; }
        public int SupplierOrderId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Remark { get; set; }//Nullable
        public DateTime CreatedAt { get; set; }

        //Foreign keys
        public int SupplierId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductBrandName { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;

}