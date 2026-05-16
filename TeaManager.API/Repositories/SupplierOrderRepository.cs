using Microsoft.EntityFrameworkCore;
using TeaManager.API.Data;
using TeaManager.API.Models.Domain;

namespace TeaManager.API.Repositories
{
    public class SupplierOrderRepository : ISupplierOrderRepository
    {
        private readonly TeaManagerDbContext _dbContext;

        public SupplierOrderRepository(TeaManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<SupplierOrder> GetAll()
        {
            return _dbContext.SupplierOrders
                .Include(so => so.Product)
                    .ThenInclude(p => p.Brand)
                .Include(so => so.Supplier)
                .OrderBy(so => so.SupplierOrderId)
                .ToList();
        }

        public SupplierOrder? GetById(int supplierOrderId)
        {
            return _dbContext.SupplierOrders
                .Include(so => so.Product)
                    .ThenInclude(p => p.Brand)
                .Include(so => so.Supplier)
                .FirstOrDefault(so => so.SupplierOrderId == supplierOrderId);
        }

        public void Add(SupplierOrder supplierOrder)
        {
            _dbContext.SupplierOrders.Add(supplierOrder);
        }

        public void Update(SupplierOrder supplierOrder)
        {
            // EF tracks automatically
        }

        public void Delete(SupplierOrder supplierOrder)
        {
            _dbContext.SupplierOrders.Remove(supplierOrder);
        }

        public int GetNextSupplierOrderId()
        {
            return _dbContext.SupplierOrders.Any()
                ? _dbContext.SupplierOrders.Max(so => so.SupplierOrderId) + 1
                : 1;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
