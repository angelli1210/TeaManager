using TeaManager.API.Models.DTO;

namespace TeaManager.API.Services
{
    public interface IBrandService
    {
        IEnumerable<BrandDTO> GetAllBrands();
        BrandDTO? GetBrandById(int brandId);
        BrandDTO CreateBrand(CreateBrandRequestDTO createDto);
        BrandDTO? UpdateBrand(int brandId, UpdateBrandRequestDTO updateDto);
        bool DeleteBrand(int brandId);
    }
}