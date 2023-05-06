using BlogJwtNet6.Authorization;
using BlogJwtNet6.Data;
using BlogJwtNet6.Dtos;
using BlogJwtNet6.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using ServiceStack.Redis;
using System.Net.Http;
using System.Text;

namespace BlogJwtNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BlogDbContext _context;
        private readonly IUserService _userService;

        public UsersController(BlogDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;


           
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser(RegisterRequest registerRequest)
        {
            var response = await _userService.register(registerRequest);
/*            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(50))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30));
            var dataToCache = "hello";
            await _cache.SetStringAsync(response.Id.ToString(), dataToCache, options);
            */
            if(response != null) {
                return Created($"/users/#{response.Id}", response);
            }
            else
            {
                return UnprocessableEntity();
            }
            
        }

        [HttpGet]
        [HotAuthorize]
        [Route("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var response = _userService.GetCurrentUser();
            return Ok(response);
        }

        [HttpDelete]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {

            await _userService.Logout();
            return NoContent();

        }
    }
}
