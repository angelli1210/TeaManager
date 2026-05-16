using TeaManager.API.Data;
using TeaManager.API.Models.Domain;

namespace TeaManager.API.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly TeaManagerDbContext _dbContext;

        public SupplierRepository(TeaManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Supplier> GetAll()
        {
            return _dbContext.Suppliers.OrderBy(s => s.SupplierId).ToList();
        }
        public Supplier? GetById(int supplierId)
        {
            return _dbContext.Suppliers.FirstOrDefault(s => s.SupplierId == supplierId);
        }
        public void Add(Supplier supplier)
        {
            _dbContext.Suppliers.Add(supplier);
        }
        public void Update(Supplier supplier)
        {
            // EF tracks automatically
        }
        public void Delete(Supplier supplier)
        {
            _dbContext.Suppliers.Remove(supplier);
        }
        public bool EmailExists(string contactEmail, int? excludeSupplierId = null) //check duplicate
        {
            return _dbContext.Suppliers.Any(s => s.ContactEmail == contactEmail && s.SupplierId != excludeSupplierId);
        }
        public int GetNextSupplierId()
        {
            return _dbContext.Suppliers.Any() ? _dbContext.Suppliers.Max(s => s.SupplierId) + 1 : 1;
        }
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}