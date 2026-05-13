namespace TeaManager.API.Models.DTO
{
    public class SupplierDTO
    {
        public Guid Id { get; set; }
        public int SupplierId { get; set; }
        public required string SupplierName { get; set; }
        public required string Country { get; set; }
        public required string ContactEmail { get; set; }
        public required string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
