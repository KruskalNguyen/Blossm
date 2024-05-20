using BlossmAPI.Models;
using BlossmAPI.Repositories.Interfaces;
using BlossmAPI.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BlossmAPI.Repositories.Services
{
	public class CategoryServices : ICategoryServices
	{
		private readonly BlossmContext _context;
		public CategoryServices(BlossmContext context)
		{
			_context = context;
		}
		public async Task<ApiResponse<List<Category>>> GetCategories()
		{
			var rs = await _context.Categories.ToListAsync();
			ApiResponse<List<Category>> response = new ApiResponse<List<Category>>();
			return response.SuccessResult(rs);
		}
	}
}
