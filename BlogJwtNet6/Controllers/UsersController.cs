using BlogJwtNet6.Authorization;
using BlogJwtNet6.Data;
using BlogJwtNet6.Dtos;
using BlogJwtNet6.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace BlogJwtNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BlogDbContext _context;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IJwtService _jwtService;

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
            if(response != null) {
                return Created($"/users/#{response.User.Id}", response);
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
    }
}
