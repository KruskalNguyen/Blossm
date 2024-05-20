using BlossmAPI.Attributes;
using BlossmAPI.Models;
using BlossmAPI.ModelViews;
using BlossmAPI.Patterns.Singleton;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlossmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [BlossmAuthorize]
    public class TestAuthorController : ControllerBase
    {
        private readonly BlossmContext _context;

        public TestAuthorController(BlossmContext context)
        {
            _context = context;
        }

        [HttpGet("Get1")]
        public async Task<string> Get1()
        {
            var id = AuthorizeSingleton.Instance.getIdUser();
            return id;
        }

        [HttpGet("Get2")]
        public async Task<string> Get2()
        {
            return AuthorizeSingleton.Instance.getIdUser();
        }

        [HttpGet("Get3")]
        public async Task Get3()
        {

        }

        [HttpGet("Get4")]
        public async Task Get4()
        {

        }
    }
}
