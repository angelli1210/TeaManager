using Microsoft.AspNetCore.Mvc;
using TeaManager.API.Models.DTO;
using TeaManager.API.Services;

namespace TeaManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var productsDto = _productService.GetAllProducts();
            return Ok(productsDto);
        }

        [HttpGet]
        [Route("{productId:int}")]
        public IActionResult GetProductById([FromRoute] int productId)
        {
            var productDto = _productService.GetProductById(productId);
            if (productDto == null)
            {
                return NotFound(new { message = $"Product with ID {productId} not found." });
            }
            return Ok(productDto);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] CreateProductRequestDTO createDto)
        {
            var productDto = _productService.CreateProduct(createDto);
            return CreatedAtAction(
                nameof(GetProductById),
                new { productId = productDto.ProductId },
                productDto
            );
        }

        [HttpPut]
        [Route("{productId:int}")]
        public IActionResult UpdateProduct(
            [FromRoute] int productId,
            [FromBody] UpdateProductRequestDTO updateDto)
        {
            var productDto = _productService.UpdateProduct(productId, updateDto);
            if (productDto == null)
            {
                return NotFound(new { message = $"Product with ID {productId} not found." });
            }
            return Ok(productDto);
        }

        [HttpDelete]
        [Route("{productId:int}")]
        public IActionResult DeleteProduct([FromRoute] int productId)
        {
            var success = _productService.DeleteProduct(productId);
            if (!success)
            {
                return NotFound(new { message = $"Product with ID {productId} not found." });
            }
            return NoContent();
        }
    }
}
