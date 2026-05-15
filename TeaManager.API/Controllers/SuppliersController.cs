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
        //GET/api/suppliers - get all suppliers
        //=================
        [HttpGet]
        public IActionResult GetAllSuppliers()
        {
            var suppliers = _dbContext.Suppliers.OrderBy(s => s.SupplierId).ToList();

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
        //GET/api/suppliers/{supplierId}- get a single supplier by id
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
        //POST/api/suppliers - create a new supplier
        //=================
        [HttpPost]
        public IActionResult CreateSupplier([FromBody] CreateSupplierRequestDTO createDto)
        {
            //Generate next SupplierId(auto-increment)
            var nextSupplierId = _dbContext.Suppliers.Any()
            ? _dbContext.Suppliers.Max(s => s.SupplierId) + 1
            : 1;
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

            _dbContext.Suppliers.Add(supplier);
            _dbContext.SaveChanges();


            //Return DTO
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
            return CreatedAtAction(
                nameof(GetSupplierById),
                new { supplierId = supplier.SupplierId },
                supplierDto
            );
        }

        //=================
        //PUT/api/suppliers/{supplierId} - update an existing supplier
        //=================
        [HttpPut]
        [Route("{supplierId:int}")]
        public IActionResult UpdateSupplier(
            [FromRoute] int supplierId,
            [FromBody] UpdateSupplierRequestDTO updateDto)
        {
            var supplier = _dbContext.Suppliers.FirstOrDefault(s => s.SupplierId == supplierId);
            if (supplier == null)
            {
                return NotFound(new { message = $"Supplier with ID {supplierId} not found." });
            }

            supplier.SupplierName = updateDto.SupplierName;
            supplier.Country = updateDto.Country;
            supplier.ContactEmail = updateDto.ContactEmail;
            supplier.Phone = updateDto.Phone;

            _dbContext.SaveChanges();

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

        //================================
        //DELETE/api/suppliers/{supplierId}
        //================================
        [HttpDelete]
        [Route("{supplierId:int}")]
        public IActionResult DeleteSupplier([FromRoute] int supplierId)
        {
            var supplier = _dbContext.Suppliers.FirstOrDefault(s => s.SupplierId == supplierId);
            if (supplier == null)
            {
                return NotFound(new { message = $"Supplier with ID {supplierId} not found." });
            }

            _dbContext.Suppliers.Remove(supplier);
            _dbContext.SaveChanges();

            return NoContent();
        }

    }
}