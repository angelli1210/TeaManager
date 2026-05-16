using Microsoft.AspNetCore.Mvc;
using TeaManager.API.Models.DTO;
using TeaManager.API.Services;

namespace TeaManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController(ISupplierService supplierService) : ControllerBase
    {
        private readonly ISupplierService _supplierService = supplierService;

        //=================
        //GET/api/suppliers - get all suppliers
        //=================
        [HttpGet]
        public IActionResult GetAllSuppliers()
        {
            var suppliersDto = _supplierService.GetAllSuppliers();
            return Ok(suppliersDto);
        }

        //=================
        //GET/api/suppliers/{supplierId}- get a single supplier by id
        //=================
        [HttpGet]
        [Route("{supplierId:int}")]
        public IActionResult GetSupplierById([FromRoute] int supplierId)
        {
            var supplierDto = _supplierService.GetSupplierById(supplierId);
            if (supplierDto == null)
            {
                return NotFound(new { message = $"Supplier with ID {supplierId} not found." });
            }
            return Ok(supplierDto);
        }

        //=================
        //POST/api/suppliers - create a new supplier
        //=================
        [HttpPost]
        public IActionResult CreateSupplier([FromBody] CreateSupplierRequestDTO createDto)
        {
            //Generate next SupplierId(auto-increment)
            var supplierDto = _supplierService.CreateSupplier(createDto);

            return CreatedAtAction(
                nameof(GetSupplierById),
                new { supplierId = supplierDto.SupplierId },
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
            var supplierDto = _supplierService.UpdateSupplier(supplierId, updateDto);
            if (supplierDto == null)
            {
                return NotFound(new { message = $"Supplier with ID {supplierId} not found." });
            }
            return Ok(supplierDto);
        }

        //================================
        //DELETE/api/suppliers/{supplierId}
        //================================
        [HttpDelete]
        [Route("{supplierId:int}")]
        public IActionResult DeleteSupplier([FromRoute] int supplierId)
        {
            var success = _supplierService.DeleteSupplier(supplierId);
            if (!success)
            {
                return NotFound(new { message = $"Supplier with ID {supplierId} not found." });
            }

            return NoContent();
        }

    }
}