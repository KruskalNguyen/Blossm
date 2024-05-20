using BlossmAPI.ModelViews;
using BlossmAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentServices _services;

        public CommentController (ICommentServices services)
        {
            _services = services;
        }
        [HttpPost("GetCommentsByUser")]
        public async Task<IActionResult> GetCommentsByUser([FromBody] string id)
        {
            var rs = await _services.GetAllCommentByUser(id);
            if(rs != null)
            {
                return Ok(rs.Data);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("GetCommentsByProduct")]
        public async Task<IActionResult> GetCommentsByProduct([FromBody]int id)
        {
            var rs = await _services.GetAllCommentByProduct(id);
            if(rs != null)
            {
                return Ok(rs.Data);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CommentView view)
        {
            var rs = await _services.Create(view);
            if(rs != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(CommentView view)
        {
            var rs = await _services.Update(view);
            if(rs != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var rs = await _services.Delete(id);

            if(rs!= null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
