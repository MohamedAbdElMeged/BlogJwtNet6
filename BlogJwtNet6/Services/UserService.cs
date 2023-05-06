using BlogJwtNet6.Data;
using BlogJwtNet6.Dtos;
using BlogJwtNet6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using ServiceStack;
using ServiceStack.Redis;
using System.Text;

namespace BlogJwtNet6.Services
{
    public class UserService : IUserService
    {
        private BlogDbContext _context;
        private IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IRedisService _redisService;

        public UserService(BlogDbContext context,IJwtService jwtService, IHttpContextAccessor httpContext, IRedisService redisService)
        {
            _context = context;
            _jwtService = jwtService;
            _httpContext = httpContext;
            _redisService = redisService;
        }

        public User GetById(string userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == Guid.Parse(userId));
            return user;
        }

        public UserResponse GetCurrentUser()
        {
            var user =  (User) _httpContext.HttpContext.Items["User"];
            var response = new UserResponse()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
            return  response;


        }

        public  async Task<RegisterResponse> register(RegisterRequest registerRequest)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                Email = registerRequest.Email,
                Password = registerRequest.Password,

            };

             await  _context.Users.AddAsync(user);
             await _context.SaveChangesAsync();

            var authToken =  _jwtService.GenerateJwtToken(user);
            var response = new RegisterResponse()
            {
                Message = "Registration Successfull",
                Id = user.Id,
                AuthToken = authToken,
            };
            return response;

        }

        public async Task Logout()
        {
            var authToken = _httpContext.HttpContext.Items["AuthToken"].ToString();
            var expiredAuthToken = Encoding.UTF8.GetBytes(authToken);
             _redisService.GetRedisClient().LPush("expiredTokens", expiredAuthToken);
        }
    }
}
