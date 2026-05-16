using Microsoft.EntityFrameworkCore;
using TeaManager.API.Data;
using TeaManager.API.Models.Domain;

namespace TeaManager.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly TeaManagerDbContext _dbContext;
        public ProductRepository(TeaManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Product> GetAll()
        {
            return _dbContext.Products
                .Include(p => p.Brand)
                .Include(p => p.Supplier)
                .OrderBy(p => p.ProductId)
                .ToList();
        }

        public Product? GetById(int productId)
        {
            return _dbContext.Products
                .Include(p => p.Brand)
                .Include(p => p.Supplier)
                .FirstOrDefault(p => p.ProductId == productId);
        }

        public void Add(Product product)
        {
            _dbContext.Products.Add(product);
        }

        public void Update(Product product)
        {
            //EF tracks automatically
        }

        public void Delete(Product product)
        {
            _dbContext.Products.Remove(product);
        }

        public int GetNextProductId()
        {
            return _dbContext.Products.Any()
            ? _dbContext.Products.Max(p => p.ProductId) + 1 : 1;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}