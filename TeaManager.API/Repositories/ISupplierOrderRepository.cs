using TeaManager.API.Models.Domain;

namespace TeaManager.API.Repositories
{
    public interface ISupplierOrderRepository
    {
        IEnumerable<SupplierOrder> GetAll();
        SupplierOrder? GetById(int supplierOrderId);
        void Add(SupplierOrder supplierOrder);
        void Update(SupplierOrder supplierOrder);
        void Delete(SupplierOrder supplierOrder);
        int GetNextSupplierOrderId();
        void SaveChanges();
    }
}
