using TeaManager.API.Models.Domain;
using TeaManager.API.Models.DTO;
using TeaManager.API.Repositories;

namespace TeaManager.API.Services
{
    public class SupplierOrderService : ISupplierOrderService
    {
        private readonly ISupplierOrderRepository _supplierOrderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISupplierRepository _supplierRepository;

        public SupplierOrderService(
            ISupplierOrderRepository supplierOrderRepository,
            IProductRepository productRepository,
            ISupplierRepository supplierRepository)
        {
            _supplierOrderRepository = supplierOrderRepository;
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
        }

        public IEnumerable<SupplierOrderDTO> GetAllSupplierOrders()
        {
            var supplierOrders = _supplierOrderRepository.GetAll();
            var supplierOrdersDto = new List<SupplierOrderDTO>();
            foreach (var supplierOrder in supplierOrders)
            {
                supplierOrdersDto.Add(new SupplierOrderDTO
                {
                    Id = supplierOrder.Id,
                    SupplierOrderId = supplierOrder.SupplierOrderId,
                    Quantity = supplierOrder.Quantity,
                    OrderDate = supplierOrder.OrderDate,
                    Remark = supplierOrder.Remark,
                    CreatedAt = supplierOrder.CreatedAt,
                    ProductId = supplierOrder.Product.ProductId,
                    ProductName = supplierOrder.Product.ProductName,
                    SupplierId = supplierOrder.Supplier.SupplierId,
                    SupplierName = supplierOrder.Supplier.SupplierName,
                    ProductBrandName = supplierOrder.Product.Brand.BrandName
                });
            }
            return supplierOrdersDto;
        }

        public SupplierOrderDTO? GetSupplierOrderById(int supplierOrderId)
        {
            var supplierOrder = _supplierOrderRepository.GetById(supplierOrderId);
            if (supplierOrder == null)
            {
                return null;
            }

            return new SupplierOrderDTO
            {
                Id = supplierOrder.Id,
                SupplierOrderId = supplierOrder.SupplierOrderId,
                Quantity = supplierOrder.Quantity,
                OrderDate = supplierOrder.OrderDate,
                Remark = supplierOrder.Remark,
                CreatedAt = supplierOrder.CreatedAt,
                ProductId = supplierOrder.Product.ProductId,
                ProductName = supplierOrder.Product.ProductName,
                SupplierId = supplierOrder.Supplier.SupplierId,
                SupplierName = supplierOrder.Supplier.SupplierName,
                ProductBrandName = supplierOrder.Product.Brand.BrandName
            };
        }

        //Validate foreign key
        public SupplierOrderDTO CreateSupplierOrder(CreateSupplierOrderRequestDTO createDto)
        {
            //Supplier FK validation
            var supplier = _supplierRepository.GetById(createDto.SupplierId);
            if (supplier == null)
            {
                throw new InvalidOperationException(
                    $"Supplier with ID {createDto.SupplierId} does not exist.");
            }

            //Product FK validation
            var product = _productRepository.GetById(createDto.ProductId);
            if (product == null)
            {
                throw new InvalidOperationException(
                    $"Product with ID {createDto.ProductId} does not exist."
                );
            }

            //SupplierOrder create
            var nextSupplierOrderId = _supplierOrderRepository.GetNextSupplierOrderId();
            var supplierOrder = new SupplierOrder
            {
                Id = Guid.NewGuid(),
                SupplierOrderId = nextSupplierOrderId,
                Quantity = createDto.Quantity,
                OrderDate = createDto.OrderDate,
                Remark = createDto.Remark,
                SupplierId = supplier.Id,
                ProductId = product.Id,
                CreatedAt = DateTime.UtcNow

            };
            _supplierOrderRepository.Add(supplierOrder);
            _supplierOrderRepository.SaveChanges();

            //DTO mapping 
            return new SupplierOrderDTO
            {
                Id = supplierOrder.Id,
                SupplierOrderId = supplierOrder.SupplierOrderId,
                Quantity = supplierOrder.Quantity,
                OrderDate = supplierOrder.OrderDate,
                Remark = supplierOrder.Remark,
                CreatedAt = supplierOrder.CreatedAt,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                ProductBrandName = product.Brand.BrandName // brand is included by GetBy Id
            };
        }

        public SupplierOrderDTO? UpdateSupplierOrder(int supplierOrderId, UpdateSupplierOrderRequestDTO updateDto)
        {
            var supplierOrder = _supplierOrderRepository.GetById(supplierOrderId);
            if (supplierOrder == null)
            {
                return null;
            }

            //FK validation
            var supplier = _supplierRepository.GetById(updateDto.SupplierId);
            if (supplier == null)
            {
                throw new InvalidOperationException(
                    $"Supplier with ID {updateDto.SupplierId} does not exist."
                );
            }

            var product = _productRepository.GetById(updateDto.ProductId);
            if (product == null)
            {
                throw new InvalidOperationException(
                    $"Product with ID {updateDto.ProductId} does not exist."
                );
            }

            //Update properties
            supplierOrder.Quantity = updateDto.Quantity;
            supplierOrder.OrderDate = updateDto.OrderDate;
            supplierOrder.Remark = updateDto.Remark;
            supplierOrder.SupplierId = supplier.Id;
            supplierOrder.ProductId = product.Id;

            _supplierOrderRepository.SaveChanges();

            return new SupplierOrderDTO
            {
                Id = supplierOrder.Id,
                SupplierOrderId = supplierOrder.SupplierOrderId,
                Quantity = supplierOrder.Quantity,
                OrderDate = supplierOrder.OrderDate,
                Remark = supplierOrder.Remark,
                CreatedAt = supplierOrder.CreatedAt,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                ProductBrandName = product.Brand.BrandName

            };
        }

        public bool DeleteSupplierOrder(int supplierOrderId)
        {
            var supplierOrder = _supplierOrderRepository.GetById(supplierOrderId);
            if (supplierOrder == null)
            {
                return false;
            }
            _supplierOrderRepository.Delete(supplierOrder);
            _supplierOrderRepository.SaveChanges();

            return true;
        }
    }
}