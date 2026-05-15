namespace TeaManager.API.Models.Domain
{
    public class Brand
    {
        public Guid Id { get; set; }
        public int BrandId { get; set; }
        public required string BrandName { get; set; }
        public required string Country { get; set; }
        public int FoundedYear { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Address { get; set; }
        public required string BusinessRegNumber { get; set; }
        public required string OwnerName { get; set; }
        public DateTime CreatedAt { get; set; }

        //Navigation properties
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}