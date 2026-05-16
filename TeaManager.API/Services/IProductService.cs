using TeaManager.API.Models.DTO;

namespace TeaManager.API.Services
{
    public interface IProductService
    {
        IEnumerable<ProductDTO> GetAllProducts();
        ProductDTO? GetProductById(int productId);
        ProductDTO CreateProduct(CreateProductRequestDTO createDto);
        ProductDTO? UpdateProduct(int productId, UpdateProductRequestDTO updateDto);
        bool DeleteProduct(int productId);
    }
}