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
        //GET/api/supplier - get all suppliers
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
        //GET/api/supplier/{supplierId}- get a single supplier by id
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
        //POST/api/supplier - create a new supplier
        //=================
        [HttpPost]
        public IActionResult CreateSupplier([FromBody] CreateSupplierRequestDTO createDto)
        {
            //SupplierId to check for duplicates
            if (_dbContext.Suppliers.Any(s => s.SupplierId == createDto.SupplierId))
            {
                return BadRequest(new { message = $"SupplierId {createDto.SupplierId} already exists." });
            }

            var supplier = new Supplier
            {
                Id = Guid.NewGuid(),
                SupplierId = createDto.SupplierId,
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
        //PUT/api/supplier/{supplierId} - update an existing supplier
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
        //DELETE/api/supplier/{supplierId}
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