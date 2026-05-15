using Microsoft.AspNetCore.Mvc;
using TeaManager.API.Data;
using TeaManager.API.Models.Domain;
using TeaManager.API.Models.DTO;

namespace TeaManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController(TeaManagerDbContext dbContext) : ControllerBase
    {
        private readonly TeaManagerDbContext _dbContext = dbContext;

        //=================
        //GET/api/brands - get all brands
        //=================
        [HttpGet]
        public IActionResult GetAllBrands()
        {
            var brands = _dbContext.Brands.ToList();

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
            return Ok(brandsDto);
        }

        //=================
        //GET/api/brands/{brandId}- get a single brand by id
        //=================
        [HttpGet]
        [Route("{brandId:int}")]
        public IActionResult GetBrandById([FromRoute] int brandId)
        {
            var brand = _dbContext.Brands.FirstOrDefault(b => b.BrandId == brandId);
            if (brand == null)
            {
                return NotFound(new { message = $"Brand with ID {brandId} not found." });
            }

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

        //=================
        //POST/api/brands - create a new brand
        //=================
        [HttpPost]
        public IActionResult CreateBrand([FromBody] CreateBrandRequestDTO createDto)
        {
            //BrandId to check duplicates
            if (_dbContext.Brands.Any(b => b.BrandId == createDto.BrandId))
            {
                return BadRequest(new { message = $"BrandId {createDto.BrandId} already exists." });
            }

            //Validate Email duplicates (+ prevent create brand account with same email)
            if (_dbContext.Brands.Any(b => b.Email == createDto.Email))
            {
                return BadRequest(new { message = $"Email '{createDto.Email}' already exists." });
            }

            var brand = new Brand
            {
                Id = Guid.NewGuid(),
                BrandId = createDto.BrandId,
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

            _dbContext.Brands.Add(brand);
            _dbContext.SaveChanges();


            //Return DTO
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
        //PUT/api/brands/{brandId} - update an existing brand
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