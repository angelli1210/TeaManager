using TeaManager.API.Models.Domain;

namespace TeaManager.API.Repositories
{
    public interface ISupplierRepository
    {
        IEnumerable<Supplier> GetAll();
        bool EmailExists(string contactEmail, int? excludeSupplierId = null);
        Supplier? GetById(int supplierId);
        void Add(Supplier supplier);
        void Update(Supplier supplier);
        void Delete(Supplier supplier);
        int GetNextSupplierId();
        void SaveChanges();
    }
}
