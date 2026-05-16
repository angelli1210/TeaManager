using TeaManager.API.Models.Domain;

namespace TeaManager.API.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product? GetById(int productId);
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        int GetNextProductId();
        void SaveChanges();
    }


}