using Microsoft.AspNetCore.Mvc;
using TeaManager.API.Models.DTO;
using TeaManager.API.Services;


namespace TeaManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierOrdersController(ISupplierOrderService supplierOrderService) : ControllerBase
    {
        private readonly ISupplierOrderService _supplierOrderService = supplierOrderService;
        //=================
        //GET/api/supplierOrders - get all SupplierOrders
        //=================
        [HttpGet]
        public IActionResult GetAllSupplierOrders()
        {
            var supplierOrdersDto = _supplierOrderService.GetAllSupplierOrders();
            return Ok(supplierOrdersDto);
        }

        //=================
        //GET/api/supplierOrders/{supplierOrderId}- get a single supplier order by id
        //=================
        [HttpGet]
        [Route("{supplierOrderId:int}")]
        public IActionResult GetSupplierOrderById([FromRoute] int supplierOrderId)
        {
            var supplierOrderDto = _supplierOrderService.GetSupplierOrderById(supplierOrderId);
            if (supplierOrderDto == null)
            {
                return NotFound(new { message = $"Supplier Order with ID {supplierOrderId} not found." });
            }
            return Ok(supplierOrderDto);
        }
        //=================
        //POST/api/supplierOrders - create a new order
        //=================
        [HttpPost]
        public IActionResult CreateSupplierOrder([FromBody] CreateSupplierOrderRequestDTO createDto)
        {
            var supplierOrderDto = _supplierOrderService.CreateSupplierOrder(createDto);
            return CreatedAtAction(
                nameof(GetSupplierOrderById),
                new { supplierOrderId = supplierOrderDto.SupplierOrderId },
                supplierOrderDto
            );
        }

        //=================
        //PUT/api/supplierOrders/{supplierOrderId} 
        //=================
        [HttpPut]
        [Route("{supplierOrderId:int}")]
        public IActionResult UpdateSupplierOrder(
            [FromRoute] int supplierOrderId,
            [FromBody] UpdateSupplierOrderRequestDTO updateDto)
        {
            var supplierOrderDto = _supplierOrderService.UpdateSupplierOrder(supplierOrderId, updateDto);
            if (supplierOrderDto == null)
            {
                return NotFound(new { message = $"Supplier Order with ID {supplierOrderId} not found." });
            }
            return Ok(supplierOrderDto);
        }

        //================================
        //DELETE/api/supplierOrders/{supplierOrderId}
        //================================
        [HttpDelete]
        [Route("{supplierOrderId:int}")]
        public IActionResult DeleteSupplierOrder([FromRoute] int supplierOrderId)
        {
            var success = _supplierOrderService.DeleteSupplierOrder(supplierOrderId);
            if (!success)
            {
                return NotFound(new { message = $"Supplier Order with ID {supplierOrderId} not found." });
            }
            return NoContent();
        }

    }
}