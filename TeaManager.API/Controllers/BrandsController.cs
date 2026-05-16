using Microsoft.AspNetCore.Mvc;
using TeaManager.API.Models.DTO;
using TeaManager.API.Services;



namespace TeaManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController(IBrandService brandService) : ControllerBase
    {
        private readonly IBrandService _brandService = brandService;
        //=================
        //GET/api/brands - get all brands
        //=================
        [HttpGet]
        public IActionResult GetAllBrands()
        {
            var brandsDto = _brandService.GetAllBrands();
            return Ok(brandsDto);
        }


        //=================
        //GET/api/brands/{brandId}- get a single brand by id
        //=================
        [HttpGet]
        [Route("{brandId:int}")]
        public IActionResult GetBrandById([FromRoute] int brandId)
        {
            var brandDto = _brandService.GetBrandById(brandId);
            if (brandDto == null)
            {
                return NotFound(new { message = $"Brand with ID {brandId} not found." });
            }
            return Ok(brandDto);
        }


        //=================
        //POST/api/brands - create a new brand
        //=================
        [HttpPost]
        public IActionResult CreateBrand([FromBody] CreateBrandRequestDTO createDto)
        {
            var brandDto = _brandService.CreateBrand(createDto);
            return CreatedAtAction(
                nameof(GetBrandById),
                new { brandId = brandDto.BrandId },
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
            var brandDto = _brandService.UpdateBrand(brandId, updateDto);
            if (brandDto == null)
            {
                return NotFound(new { message = $"Brand with ID {brandId} not found." });
            }
            return Ok(brandDto);
        }


        //================================
        //DELETE/api/brands/{brandId}
        //================================
        [HttpDelete]
        [Route("{brandId:int}")]
        public IActionResult DeleteBrand([FromRoute] int brandId)
        {
            var success = _brandService.DeleteBrand(brandId);
            if (!success)
            {
                return NotFound(new { message = $"Brand with ID {brandId} not found." });
            }
            return NoContent();
        }


    }
}