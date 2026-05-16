using TeaManager.API.Models.Domain;
using TeaManager.API.Models.DTO;
using TeaManager.API.Repositories;

namespace TeaManager.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly ISupplierRepository _supplierRepository;

        public ProductService(
            IProductRepository productRepository,
            IBrandRepository brandRepository,
            ISupplierRepository supplierRepository)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _supplierRepository = supplierRepository;
        }

        //========
        //GET - get all prodcutIDs 
        //=========
        public IEnumerable<ProductDTO> GetAllProducts()
        {
            var products = _productRepository.GetAll();
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
            return productsDto;
        }

        //========
        //GET - get a single product by ID 
        //=========
        public ProductDTO? GetProductById(int productId)
        {
            var product = _productRepository.GetById(productId);
            if (product == null) return null;

            return new ProductDTO
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
        }

        //========
        //POST
        //=========
        public ProductDTO CreateProduct(CreateProductRequestDTO createDto)
        {
            //Validate Brand FK
            var brand = _brandRepository.GetById(createDto.BrandId);
            if (brand == null)
            {
                throw new InvalidOperationException(
                    $"Brand with ID {createDto.BrandId} does not exist."
                );
            }
            //Supplier FK validation
            var supplier = _supplierRepository.GetById(createDto.SupplierId);
            if (supplier == null)
            {
                throw new InvalidOperationException(
                    $"Supplier with ID {createDto.SupplierId} does not exist.");
            }

            //Generate Product
            var nextProductId = _productRepository.GetNextProductId();
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
                BrandId = brand.Id, //Guid FK (Not Brand.Id or BrandId!)
                SupplierId = supplier.Id //Guid FK (NOT Supplier.Id or Supplier Id!)

            };
            _productRepository.Add(product);
            _productRepository.SaveChanges();

            //DTO mapping 
            return new ProductDTO
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
                BrandId = brand.BrandId, //int (Business key)
                BrandName = brand.BrandName,
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName
            };
        }

        //========
        //Put
        //=========
        public ProductDTO? UpdateProduct(int productId, UpdateProductRequestDTO updateDto)
        {
            var product = _productRepository.GetById(productId);
            if (product == null)
            {
                return null;
            }

            //Validate FK 
            var brand = _brandRepository.GetById(updateDto.BrandId);
            if (brand == null)
            {
                throw new InvalidOperationException(
                    $"Brand with ID {updateDto.BrandId} does not exist.");
            }

            var supplier = _supplierRepository.GetById(updateDto.SupplierId);
            if (supplier == null)
            {
                throw new InvalidOperationException(
                    $"Supplier with ID {updateDto.SupplierId} does not exist.");
            }

            //Update properties
            product.ProductName = updateDto.ProductName;
            product.Description = updateDto.Description;
            product.Price = updateDto.Price;
            product.Stock = updateDto.Stock;
            product.HarvestYear = updateDto.HarvestYear;
            product.Origin = updateDto.Origin;
            product.BrandId = brand.Id;          // Guid FK
            product.SupplierId = supplier.Id;   //Guid FK

            _productRepository.SaveChanges();

            return new ProductDTO
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

        }

        //Delete
        public bool DeleteProduct(int productId)
        {
            var product = _productRepository.GetById(productId);
            if (product == null) return false;

            _productRepository.Delete(product);
            _productRepository.SaveChanges();

            return true;
        }


    }
}
