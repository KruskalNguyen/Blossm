using BlossmAPI.Models;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewspaperController : ControllerBase
    {
        private readonly INewspaperServices _newspaperServices;

        public NewspaperController(INewspaperServices newspaperServices)
        {
            _newspaperServices = newspaperServices;
        }

        [HttpGet("GetNewspaper")]
        public async Task<IActionResult> GetNewspaper()
        {
            var rs = await _newspaperServices.GetNewspaper();
            return Ok(rs);
        }

        [HttpPost("CreateNewspaper")]
        public async Task<IActionResult> CreateNewspaper(Newspaper newspaper)
        {
            var rs = await _newspaperServices.CreateNewspaper(newspaper);
            return Ok(rs);
        }

        [HttpPut("UpdateNewspaper")]
        public async Task<IActionResult> UpdateNewspaper(Newspaper newspaper)
        {
            var rs = await _newspaperServices.UpdateNewspaper(newspaper);
            return Ok(rs);
        }

        [HttpDelete("DeleteNewspaper")]
        public async Task<IActionResult> DeleteNewspaper(int id)
        {
            var rs = await _newspaperServices.DeleteNewspaper(id);
            return Ok(rs);
        }
    }
}
