using BlossmAPI.Models;
using BlossmAPI.Utilities;

namespace BlossmAPI.Repositories.Interfaces
{
	public interface ICategoryServices
	{
		public Task<ApiResponse<List<Category>>> GetCategories();
	}
}
