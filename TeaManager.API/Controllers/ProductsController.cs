using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TeaManager.API.Data;
using TeaManager.API.Models.Domain;
using TeaManager.API.Models.DTO;

namespace TeaManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(TeaManagerDbContext dbContext) : ControllerBase
    {
        private readonly TeaManagerDbContext _dbContext = dbContext;

        //=================
        //GET/api/products - get all products
        //=================
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _dbContext.Products
                .Include(p => p.Brand) //Join with brands
                .Include(p => p.Supplier) // Join with suppliers
                .ToList();
            var productsDto = new List<ProductDTO>();
            foreach (var product in products)
            {
                productsDto.Add(new ProductDTO
                {
                    Id = product.Id,
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    HarvestYear = product.HarvestYear,
                    Origin = product.Origin,
                    CreatedAt = product.CreatedAt,
                    BrandId = product.Brand.BrandId,
                    BrandName = product.Brand.BrandName,
                    SupplierId = product.Supplier.SupplierId,
                    SupplierName = product.Supplier.SupplierName
                });
            }
            return Ok(productsDto);
        }

        //=================
        //GET/api/products/{productId}- get a single Product by id
        //=================
        [HttpGet]
        [Route("{productId:int}")]
        public IActionResult GetProductById([FromRoute] int productId)
        {
            var product = _dbContext.Products
                .Include(p => p.Brand) //Join with brands
                .Include(p => p.Supplier) // Join with suppliers
                .FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
            {
                return NotFound(new { message = $"Product with ID {productId} not found." });
            }

            var productDto = new ProductDTO
            {
                Id = product.Id,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                HarvestYear = product.HarvestYear,
                Origin = product.Origin,
                CreatedAt = product.CreatedAt,
                BrandId = product.Brand.BrandId,
                BrandName = product.Brand.BrandName,
                SupplierId = product.Supplier.SupplierId,
                SupplierName = product.Supplier.SupplierName
            };
            return Ok(productDto);
        }

        //=================
        //POST/api/products - create a new Product
        //=================
        [HttpPost]
        public IActionResult CreateProduct([FromBody] CreateProductRequestDTO createDto)
        {
            //Find brand by BrandId(int)
            var brand = _dbContext.Brands.FirstOrDefault(b => b.BrandId == createDto.BrandId);
            if (brand == null)
            {
                return BadRequest(new { message = $"Brand with ID {createDto.BrandId} does not exist." });
            }

            //Find Supplier by SupplierId(int)
            var supplier = _dbContext.Suppliers.FirstOrDefault(s => s.SupplierId == createDto.SupplierId);
            if (supplier == null)
            {
                return BadRequest(new { message = $"Supplier with ID {createDto.SupplierId} does not exist." });
            }

            //Generate next ProductId (auto-increment)
            var nextProductId = _dbContext.Products.Any()
            ? _dbContext.Products.Max(p => p.ProductId) + 1
            : 1;

            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductId = nextProductId,
                ProductName = createDto.ProductName,
                Description = createDto.Description,
                Price = createDto.Price,
                Stock = createDto.Stock,
                HarvestYear = createDto.HarvestYear,
                Origin = createDto.Origin,
                CreatedAt = DateTime.UtcNow,
                BrandId = brand.Id, // FK,
                SupplierId = supplier.Id //FK
            };

            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();


            //Return DTO
            var productDto = new ProductDTO
            {
                Id = product.Id,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                HarvestYear = product.HarvestYear,
                Origin = product.Origin,
                CreatedAt = product.CreatedAt,
                BrandId = brand.BrandId,
                BrandName = brand.BrandName,
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName

            };
            return CreatedAtAction(
                nameof(GetProductById),
                new { productId = product.ProductId },
                productDto
            );
        }

        //=================
        //PUT/api/Products/{ProductId} - update an existing Product
        //=================
        [HttpPut]
        [Route("{productId:int}")]
        public IActionResult UpdateProduct(
            [FromRoute] int productId,
            [FromBody] UpdateProductRequestDTO updateDto)
        {

            var product = _dbContext.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
            {
                return NotFound(new { message = $"Product with ID {productId} not found." });
            }

            //Validate FK 
            var brand = _dbContext.Brands.FirstOrDefault(b => b.BrandId == updateDto.BrandId);
            if (brand == null)
            {
                return BadRequest(new { message = $"Brand with ID {updateDto.BrandId} does not exist." });

            }

            var supplier = _dbContext.Suppliers.FirstOrDefault(s => s.SupplierId == updateDto.SupplierId);
            if (supplier == null)
            {
                return BadRequest(new { message = $"Supplier with ID {updateDto.SupplierId} does not exist." });
            }

            product.ProductName = updateDto.ProductName;
            product.Description = updateDto.Description;
            product.Price = updateDto.Price;
            product.Stock = updateDto.Stock;
            product.HarvestYear = updateDto.HarvestYear;
            product.Origin = updateDto.Origin;
            product.BrandId = brand.Id; //FK
            product.SupplierId = supplier.Id; //FK
            _dbContext.SaveChanges();

            var productDto = new ProductDTO
            {
                Id = product.Id,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                HarvestYear = product.HarvestYear,
                Origin = product.Origin,
                CreatedAt = product.CreatedAt,
                BrandId = brand.BrandId,
                BrandName = brand.BrandName,
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName

            };

            return Ok(productDto);
        }

        //================================
        //DELETE/api/Products/{ProductId}
        //================================
        [HttpDelete]
        [Route("{productId:int}")]
        public IActionResult DeleteProduct([FromRoute] int productId)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
            {
                return NotFound(new { message = $"Product with ID {productId} not found." });
            }

            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();

            return NoContent();
        }

    }
}