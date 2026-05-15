using TeaManager.API.Data;
using TeaManager.API.Models.Domain;

namespace TeaManager.API.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly TeaManagerDbContext _dbContext;

        public BrandRepository(TeaManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Brand> GetAll()
        {
            return _dbContext.Brands.OrderBy(b => b.BrandId).ToList();
        }
        public Brand? GetById(int brandId)
        {
            return _dbContext.Brands.FirstOrDefault(b => b.BrandId == brandId);
        }
        public void Add(Brand brand)
        {
            _dbContext.Brands.Add(brand);
        }
        public void Update(Brand brand)
        {
            // EF tracks automatically
        }
        public void Delete(Brand brand)
        {
            _dbContext.Brands.Remove(brand);
        }
        public bool EmailExists(string email, int? excludeBrandId = null) //check duplicate
        {
            return _dbContext.Brands.Any(b => b.Email == email && b.BrandId != excludeBrandId);
        }
        public int GetNextBrandId()
        {
            return _dbContext.Brands.Any() ? _dbContext.Brands.Max(b => b.BrandId) + 1 : 1;
        }
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}