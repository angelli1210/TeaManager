using TeaManager.API.Models.Domain;

namespace TeaManager.API.Repositories
{
    public interface IBrandRepository
    {
        IEnumerable<Brand> GetAll();
        Brand? GetById(int brandId);
        void Add(Brand brand);
        void Update(Brand brand);
        void Delete(Brand brand);
        bool EmailExists(string email, int? excludeBrandId = null); //check duplicate
        int GetNextBrandId();
        void SaveChanges();
    }
}

