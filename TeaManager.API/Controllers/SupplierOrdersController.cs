using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TeaManager.API.Data;
using TeaManager.API.Models.Domain;
using TeaManager.API.Models.DTO;

namespace TeaManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierOrdersController(TeaManagerDbContext dbContext) : ControllerBase
    {
        private readonly TeaManagerDbContext _dbContext = dbContext;

        //=================
        //GET/api/supplierOrders - get all SupplierOrders
        //=================
        [HttpGet]
        public IActionResult GetAllSupplierOrders()
        {
            var supplierOrders = _dbContext.SupplierOrders
                .Include(so => so.Product) //Join with products   
                .Include(so => so.Supplier) //Join with suppliers
                .ToList();
            var supplierOrdersDto = new List<SupplierOrderDTO>();
            foreach (var supplierOrder in supplierOrders)
            {
                supplierOrdersDto.Add(new SupplierOrderDTO
                {
                    Id = supplierOrder.Id,
                    SupplierOrderId = supplierOrder.SupplierOrderId,
                    Quantity = supplierOrder.Quantity,
                    OrderDate = supplierOrder.OrderDate,
                    Remark = supplierOrder.Remark,
                    CreatedAt = supplierOrder.CreatedAt,
                    ProductId = supplierOrder.Product.ProductId,
                    ProductName = supplierOrder.Product.ProductName,
                    SupplierId = supplierOrder.Supplier.SupplierId,
                    SupplierName = supplierOrder.Supplier.SupplierName,


                });
            }
            return Ok(supplierOrdersDto);
        }

        //=================
        //GET/api/supplierOrders/{supplierOrderId}- get a single supplier order by id
        //=================
        [HttpGet]
        [Route("{supplierOrderId:int}")]
        public IActionResult GetSupplierOrderById([FromRoute] int supplierOrderId)
        {
            var supplierOrder = _dbContext.SupplierOrders
                .Include(p => p.Product) //Join with brands
                .Include(s => s.Supplier) // Join with suppliers
                .FirstOrDefault(so => so.SupplierOrderId == supplierOrderId);
            if (supplierOrder == null)
            {
                return NotFound(new { message = $"Supplier Order with ID {supplierOrderId} not found." });
            }

            var supplierOrdersDto = new SupplierOrderDTO
            {
                Id = supplierOrder.Id,
                SupplierOrderId = supplierOrder.SupplierOrderId,
                Quantity = supplierOrder.Quantity,
                OrderDate = supplierOrder.OrderDate,
                Remark = supplierOrder.Remark,
                CreatedAt = supplierOrder.CreatedAt,
                ProductId = supplierOrder.Product.ProductId,
                ProductName = supplierOrder.Product.ProductName,
                SupplierId = supplierOrder.Supplier.SupplierId,
                SupplierName = supplierOrder.Supplier.SupplierName
            };
            return Ok(supplierOrdersDto);
        }

        //=================
        //POST/api/Supplier Order - create a new order
        //=================
        [HttpPost]
        public IActionResult CreateSupplierOrder([FromBody] CreateSupplierOrderRequestDTO createDto)
        {
            //Validate Supplier ID
            var supplier = _dbContext.Suppliers.FirstOrDefault(s => s.SupplierId == createDto.SupplierId);
            if (supplier == null)
            {
                return BadRequest(new { message = $"Supplier with ID {createDto.SupplierId} does not exist." });
            }

            //Validate Product
            var product = _dbContext.Products.FirstOrDefault(p => p.ProductId == createDto.ProductId);
            if (product == null)
            {
                return BadRequest(new { message = $"Product with ID {createDto.ProductId} does not exist." });
            }

            //Geneate next SupplierOrderId (auto-increment)
            var nextSupplierOrderId = _dbContext.SupplierOrders.Any()
            ? _dbContext.SupplierOrders.Max(so => so.SupplierOrderId) + 1
            : 1;

            var supplierOrder = new SupplierOrder
            {
                Id = Guid.NewGuid(),
                SupplierOrderId = nextSupplierOrderId,
                Quantity = createDto.Quantity,
                OrderDate = createDto.OrderDate,
                Remark = createDto.Remark, // null
                SupplierId = supplier.Id,//FK (Guid)
                ProductId = product.Id,  //FK(Guid)
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.SupplierOrders.Add(supplierOrder);
            _dbContext.SaveChanges();


            //Return DTO
            var supplierOrderDto = new SupplierOrderDTO
            {
                Id = supplierOrder.Id,
                SupplierOrderId = supplierOrder.SupplierOrderId,
                Quantity = supplierOrder.Quantity,
                OrderDate = supplierOrder.OrderDate,
                Remark = supplierOrder.Remark,
                CreatedAt = supplierOrder.CreatedAt,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName
            };
            return CreatedAtAction(
                nameof(GetSupplierOrderById),
                new { supplierOrderId = supplierOrder.SupplierOrderId },
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

            var supplierOrder = _dbContext.SupplierOrders.FirstOrDefault(so => so.SupplierOrderId == supplierOrderId);
            if (supplierOrder == null)
            {
                return NotFound(new { message = $"Supplier Order with ID {supplierOrderId} not found." });
            }

            //Validate FK 
            var supplier = _dbContext.Suppliers.FirstOrDefault(s => s.SupplierId == updateDto.SupplierId);
            if (supplier == null)
            {
                return BadRequest(new { message = $"Supplier with ID {updateDto.SupplierId} does not exist." });

            }

            var product = _dbContext.Products.FirstOrDefault(p => p.ProductId == updateDto.ProductId);
            if (product == null)
            {
                return BadRequest(new { message = $"Product with ID {updateDto.ProductId} does not exist." });
            }

            supplierOrder.Quantity = updateDto.Quantity;
            supplierOrder.OrderDate = updateDto.OrderDate;
            supplierOrder.Remark = updateDto.Remark;
            supplierOrder.SupplierId = supplier.Id; //FK
            supplierOrder.ProductId = product.Id; //FK
            _dbContext.SaveChanges();

            var supplierOrderDto = new SupplierOrderDTO
            {
                Id = supplierOrder.Id,
                SupplierOrderId = supplierOrder.SupplierOrderId,
                Quantity = supplierOrder.Quantity,
                OrderDate = supplierOrder.OrderDate,
                Remark = supplierOrder.Remark,
                CreatedAt = supplierOrder.CreatedAt,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName
            };

            return Ok(supplierOrderDto);
        }

        //================================
        //DELETE/api/supplierOrders/{supplierOrderId}
        //================================
        [HttpDelete]
        [Route("{supplierOrderId:int}")]
        public IActionResult DeleteSupplierOrder([FromRoute] int supplierOrderId)
        {
            var supplierOrder = _dbContext.SupplierOrders.FirstOrDefault(so => so.SupplierOrderId == supplierOrderId);
            if (supplierOrder == null)
            {
                return NotFound(new { message = $"Supplier Order with ID {supplierOrderId} not found." });
            }

            _dbContext.SupplierOrders.Remove(supplierOrder);
            _dbContext.SaveChanges();

            return NoContent();
        }

    }
}