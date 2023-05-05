using BlogJwtNet6.Dtos;
using BlogJwtNet6.Models;

namespace BlogJwtNet6.Services
{
    public interface IUserService
    {
        public Task<RegisterResponse> register(RegisterRequest registerRequest);
        public User GetById(string userId);

        public UserResponse GetCurrentUser();

    }
}
