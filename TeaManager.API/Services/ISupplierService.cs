using TeaManager.API.Models.DTO;

namespace TeaManager.API.Services
{
    public interface ISupplierService
    {
        IEnumerable<SupplierDTO> GetAllSuppliers();
        SupplierDTO? GetSupplierById(int supplierId);
        SupplierDTO CreateSupplier(CreateSupplierRequestDTO createDto);
        SupplierDTO? UpdateSupplier(int supplierId, UpdateSupplierRequestDTO updateDto);
        bool DeleteSupplier(int supplierId);
    }
}