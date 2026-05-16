using TeaManager.API.Models.DTO;
using TeaManager.API.Models.Domain;
using TeaManager.API.Repositories;

namespace TeaManager.API.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public IEnumerable<SupplierDTO> GetAllSuppliers()
        {
            var suppliers = _supplierRepository.GetAll();
            var suppliersDto = new List<SupplierDTO>();
            foreach (var supplier in suppliers)
            {
                suppliersDto.Add(new SupplierDTO
                {
                    Id = supplier.Id,
                    SupplierId = supplier.SupplierId,
                    SupplierName = supplier.SupplierName,
                    Country = supplier.Country,
                    ContactEmail = supplier.ContactEmail,
                    Phone = supplier.Phone,
                    CreatedAt = supplier.CreatedAt
                });
            }
            return suppliersDto;
        }

        public SupplierDTO? GetSupplierById(int supplierId)
        {
            var supplier = _supplierRepository.GetById(supplierId);
            if (supplier == null) return null;
            return new SupplierDTO
            {
                Id = supplier.Id,
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                Country = supplier.Country,
                ContactEmail = supplier.ContactEmail,
                Phone = supplier.Phone,
                CreatedAt = supplier.CreatedAt
            };
        }

        public SupplierDTO CreateSupplier(CreateSupplierRequestDTO createDto)
        {

            //Email duplication validation
            if (_supplierRepository.EmailExists(createDto.ContactEmail))
            {
                throw new InvalidOperationException(
                    $"Email '{createDto.ContactEmail}' already exists."
                );
            }

            var nextSupplierId = _supplierRepository.GetNextSupplierId();
            var supplier = new Supplier
            {
                Id = Guid.NewGuid(),
                SupplierId = nextSupplierId,
                SupplierName = createDto.SupplierName,
                Country = createDto.Country,
                ContactEmail = createDto.ContactEmail,
                Phone = createDto.Phone,
                CreatedAt = DateTime.UtcNow
            };

            _supplierRepository.Add(supplier);
            _supplierRepository.SaveChanges();

            return new SupplierDTO
            {
                Id = supplier.Id,
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                Country = supplier.Country,
                ContactEmail = supplier.ContactEmail,
                Phone = supplier.Phone,
                CreatedAt = supplier.CreatedAt
            };
        }

        public SupplierDTO? UpdateSupplier(int supplierId, UpdateSupplierRequestDTO updateDto)
        {
            var supplier = _supplierRepository.GetById(supplierId);
            if (supplier == null) return null;

            if (_supplierRepository.EmailExists(updateDto.ContactEmail, supplierId))
            {
                throw new InvalidOperationException(
                    $"Email '{updateDto.ContactEmail}' is already used by another supplier.");
            }

            supplier.SupplierName = updateDto.SupplierName;
            supplier.Country = updateDto.Country;
            supplier.ContactEmail = updateDto.ContactEmail;
            supplier.Phone = updateDto.Phone;

            _supplierRepository.SaveChanges();

            return new SupplierDTO
            {
                Id = supplier.Id,
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                Country = supplier.Country,
                ContactEmail = supplier.ContactEmail,
                Phone = supplier.Phone,
                CreatedAt = supplier.CreatedAt
            };

        }

        public bool DeleteSupplier(int supplierId)
        {
            var supplier = _supplierRepository.GetById(supplierId);
            if (supplier == null) return false;

            _supplierRepository.Delete(supplier);
            _supplierRepository.SaveChanges();

            return true;
        }
    }

}


