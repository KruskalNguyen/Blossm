using BlossmAPI.Attributes;
using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productService;
        private readonly IProductVariantServices _productVariantServices;
        private readonly IUserServices _userServices;
        public ProductController(IProductServices productService, IProductVariantServices variantServices, IUserServices userServices)
        {
            _productService = productService;
            _productVariantServices = variantServices;
            _userServices = userServices;
        }
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var rs = await _productService.GetProducts();
            return Ok(rs);
        }
        /*[HttpPost("GetProductsByUserBranch")]
        public async Task<IActionResult> GetProductsByUserBranch()
        {
			var idUser = await _userServices.GetCurrentIdUser(HttpContext.User);

		}*/
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(ProductView view)
        {
            var rs = await _productService.UpdateProduct(view);
            if(rs) return Ok();
            else return BadRequest();
        }
        [HttpPost("GetByProductId")]
        public async Task<IActionResult> GetByProductId([FromBody] int id)
        {
            var rs = await _productVariantServices.GetVariantsByProductId(id);
            if (rs != null) return Ok(rs);
            else return BadRequest();
        }
        [HttpPost("GetBy")]
        public IActionResult GetBy([FromBody] ProductFilterView filtered_product)
        {
            try
            {
                var lst = _productService.GetVariants(filtered_product);
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
            
        }
        [HttpPost("Create")]
        //[BlossmAuthorize]
        public async Task<IActionResult> Create([FromBody] ProductView new_product)
        {
            var rs = await _productService.CreateProduct(new_product);
            if(rs) return Ok();
            else return BadRequest();
        }
        [HttpPost("CreateVariant")]
        public async Task<IActionResult> CreateVariant([FromBody] ProductView new_product)
        {
            var rs = await _productVariantServices.CreateVariant(new_product);
            if (rs) return Ok();
            else return BadRequest();
        }
        [HttpPut("UpdateVariant")]
        public async Task<IActionResult> UpdateVariant([FromBody] ProductView updated_variant)
        {
            var rs = await _productVariantServices.UpdateVariant(updated_variant);
            if (rs) return Ok();
            else return BadRequest();
        }
    }
}
