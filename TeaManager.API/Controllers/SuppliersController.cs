using Microsoft.AspNetCore.Mvc;
using TeaManager.API.Data;
using TeaManager.API.Models.Domain;
using TeaManager.API.Models.DTO;

namespace TeaManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController(TeaManagerDbContext dbContext) : ControllerBase
    {
        private readonly TeaManagerDbContext _dbContext = dbContext;

        //=================
        //GET/api/brand - get all brands
        //=================
        [HttpGet]
        public IActionResult GetAllSuppliers()
        {
            var suppliers = _dbContext.Suppliers.ToList();

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
            return Ok(suppliersDto);
        }

        //=================
        //GET/api/brand/{brandId}- get a single supplier by id
        //=================
        [HttpGet]
        [Route("{supplierId:int}")]
        public IActionResult GetSupplierById([FromRoute] int supplierId)
        {
            var supplier = _dbContext.Suppliers.FirstOrDefault(s => s.SupplierId == supplierId);
            if (supplier == null)
            {
                return NotFound(new { message = $"Supplier with ID {supplierId} not found." });
            }

            var supplierDto = new SupplierDTO
            {
                Id = supplier.Id,
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                Country = supplier.Country,
                ContactEmail = supplier.ContactEmail,
                Phone = supplier.Phone,
                CreatedAt = supplier.CreatedAt
            };
            return Ok(supplierDto);
        }

        //=================
        //POST/api/brand - create a new supplier
        //=================
        [HttpPost]
        public IActionResult CreateSupplier([FromBody] CreateSupplierRequestDTO createDto)
        {
            //BrandId to check for BrandId duplicates
            if (_dbContext.Suppliers.Any(s => s.SupplierId == createDto.SupplierId))
            {
                return BadRequest(new { message = $"BrandId {createDto.SupplierId} already exists." });
            }

            //Validate Email duplicates (+ prevent create brand account with same email)
            if (_dbContext.Suppliers.Any(s => s.ContactEmail == createDto.ContactEmail))
            {
                return BadRequest(new { message = $"Email '{createDto.ContactEmail}' already exists." });
            }


            var supplier = new Supplier
            {
                Id = Guid.NewGuid(),
                SupplierId = createDto.SupplierId,
                SupplierName = createDto.SupplierName,
                Country = createDto.Country,
                FoundedYear = createDto.FoundedYear,
                Email = createDto.Email,
                Password = hashedPassword,
                Phone = createDto.Phone,
                Address = createDto.Address,
                BusinessRegNumber = createDto.BusinessRegNumber,
                OwnerName = createDto.OwnerName,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.Brands.Add(brand);
            _dbContext.SaveChanges();


            //Return DTO (Exclude password)
            var brandDto = new BrandDTO
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
            return CreatedAtAction(
                nameof(GetBrandById),
                new { brandId = brand.BrandId },
                brandDto
            );
        }

        //=================
        //PUT/api/brand/{brandId} - update an existing brand (with hashed password)
        //=================
        [HttpPut]
        [Route("{brandId:int}")]
        public IActionResult UpdateBrand(
            [FromRoute] int brandId,
            [FromBody] UpdateBrandRequestDTO updateDto)
        {
            var brand = _dbContext.Brands.FirstOrDefault(b => b.BrandId == brandId);
            if (brand == null)
            {
                return NotFound(new { message = $"Brand with ID {brandId} not found." });
            }
            //Check for email duplicates (if update email)
            if (_dbContext.Brands.Any(b => b.Email == updateDto.Email && b.BrandId != brandId))
            {
                return BadRequest(new { message = $"Email '{updateDto.Email}' is already used by another brand." });
            }

            brand.BrandName = updateDto.BrandName;
            brand.Country = updateDto.Country;
            brand.FoundedYear = updateDto.FoundedYear;
            brand.Email = updateDto.Email;
            brand.Password = BCrypt.Net.BCrypt.HashPassword(updateDto.Password);
            brand.Phone = updateDto.Phone;
            brand.Address = updateDto.Address;
            brand.BusinessRegNumber = updateDto.BusinessRegNumber;
            brand.OwnerName = updateDto.OwnerName;

            _dbContext.SaveChanges();

            var brandDto = new BrandDTO
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

            return Ok(brandDto);
        }

        //================================
        //DELETE/api/brands/{brandId}
        //================================
        [HttpDelete]
        [Route("{brandId:int}")]
        public IActionResult DeleteBrand([FromRoute] int brandId)
        {
            var brand = _dbContext.Brands.FirstOrDefault(b => b.BrandId == brandId);
            if (brand == null)
            {
                return NotFound(new { message = $"Brand with ID {brandId} not found." });
            }

            _dbContext.Brands.Remove(brand);
            _dbContext.SaveChanges();

            return NoContent();
        }

    }
}