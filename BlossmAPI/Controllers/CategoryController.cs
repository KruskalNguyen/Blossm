using BlossmAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlossmAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryServices _services;
		public CategoryController(ICategoryServices services)
		{
			_services = services;
		}
		[HttpGet("GetCategories")]
		public async Task<IActionResult> GetCategories()
		{
			var rs = await _services.GetCategories();
			if (rs != null) return Ok(rs.Data);
			else return BadRequest();
		}
	}
}
