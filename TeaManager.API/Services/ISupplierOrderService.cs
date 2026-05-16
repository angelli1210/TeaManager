using TeaManager.API.Models.DTO;

namespace TeaManager.API.Services
{
    public interface ISupplierOrderService
    {
        IEnumerable<SupplierOrderDTO> GetAllSupplierOrders();
        SupplierOrderDTO? GetSupplierOrderById(int supplierOrderId);
        SupplierOrderDTO CreateSupplierOrder(CreateSupplierOrderRequestDTO createDto);
        SupplierOrderDTO? UpdateSupplierOrder(int supplierOrderId, UpdateSupplierOrderRequestDTO updateDto);
        bool DeleteSupplierOrder(int supplierOrderId);
    }
}