using TeaManager.API.Models.Domain;
using TeaManager.API.Models.DTO;
using TeaManager.API.Repositories;

namespace TeaManager.API.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public IEnumerable<BrandDTO> GetAllBrands()
        {
            var brands = _brandRepository.GetAll();
            var brandsDto = new List<BrandDTO>();
            foreach (var brand in brands)
            {
                brandsDto.Add(new BrandDTO
                {
                    Id = brand.Id,
                    BrandId = brand.BrandId,
                    BrandName = brand.BrandName,
                    Country = brand.Country,
                    FoundedYear = brand.FoundedYear,
                    Email = brand.Email,
                    Phone = brand.Phone,
                    Address = brand.Address,
                    BusinessRegNumber = brand.BusinessRegNumber,
                    OwnerName = brand.OwnerName,
                    CreatedAt = brand.CreatedAt
                });
            }
            return brandsDto;
        }

        public BrandDTO? GetBrandById(int brandId)
        {
            var brand = _brandRepository.GetById(brandId);
            if (brand == null)
            {
                return null;
            }
            return new BrandDTO
            {
                Id = brand.Id,
                BrandId = brand.BrandId,
                BrandName = brand.BrandName,
                Country = brand.Country,
                FoundedYear = brand.FoundedYear,
                Email = brand.Email,
                Phone = brand.Phone,
                Address = brand.Address,
                BusinessRegNumber = brand.BusinessRegNumber,
                OwnerName = brand.OwnerName,
                CreatedAt = brand.CreatedAt
            };
        }

        public BrandDTO CreateBrand(CreateBrandRequestDTO createDto)
        {
            if (_brandRepository.EmailExists(createDto.Email))
            {
                throw new InvalidOperationException(
                    $"Email '{createDto.Email}' already exists.");
            }
            var nextBrandId = _brandRepository.GetNextBrandId();
            var brand = new Brand
            {
                Id = Guid.NewGuid(),
                BrandId = nextBrandId,
                BrandName = createDto.BrandName,
                Country = createDto.Country,
                FoundedYear = createDto.FoundedYear,
                Email = createDto.Email,
                Phone = createDto.Phone,
                Address = createDto.Address,
                BusinessRegNumber = createDto.BusinessRegNumber,
                OwnerName = createDto.OwnerName,
                CreatedAt = DateTime.UtcNow
            };
            _brandRepository.Add(brand);
            _brandRepository.SaveChanges();

            return new BrandDTO
            {
                Id = brand.Id,
                BrandId = brand.BrandId,
                BrandName = brand.BrandName,
                Country = brand.Country,
                FoundedYear = brand.FoundedYear,
                Email = brand.Email,
                Phone = brand.Phone,
                Address = brand.Address,
                BusinessRegNumber = brand.BusinessRegNumber,
                OwnerName = brand.OwnerName,
                CreatedAt = brand.CreatedAt
            };
        }

        public BrandDTO? UpdateBrand(int brandId, UpdateBrandRequestDTO updateDto)
        {
            var brand = _brandRepository.GetById(brandId);
            if (brand == null)//check null
            {
                return null;
            }

            //Email duplication vaildaiton(excluding itself)
            if (_brandRepository.EmailExists(updateDto.Email, brandId))
            {
                throw new InvalidOperationException(
                    $"Email '{updateDto.Email}' is already used by another brand.");
            }

            brand.BrandName = updateDto.BrandName;
            brand.Country = updateDto.Country;
            brand.FoundedYear = updateDto.FoundedYear;
            brand.Email = updateDto.Email;
            brand.Phone = updateDto.Phone;
            brand.Address = updateDto.Address;
            brand.BusinessRegNumber = updateDto.BusinessRegNumber;
            brand.OwnerName = updateDto.OwnerName;

            _brandRepository.SaveChanges();

            return new BrandDTO
            {
                Id = brand.Id,
                BrandId = brand.BrandId,
                BrandName = brand.BrandName,
                Country = brand.Country,
                FoundedYear = brand.FoundedYear,
                Email = brand.Email,
                Phone = brand.Phone,
                Address = brand.Address,
                BusinessRegNumber = brand.BusinessRegNumber,
                OwnerName = brand.OwnerName,
                CreatedAt = brand.CreatedAt

            };
        }

        public bool DeleteBrand(int brandId)
        {
            var brand = _brandRepository.GetById(brandId);
            if (brand == null)
            {
                return false;
            }

            _brandRepository.Delete(brand);
            _brandRepository.SaveChanges();

            return true;
        }

    }
}