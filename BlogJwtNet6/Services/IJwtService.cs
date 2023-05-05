using BlogJwtNet6.Models;

namespace BlogJwtNet6.Services
{
    public interface IJwtService
    {
        public string GenerateJwtToken(User user);

        public string? ValidateToken(string token);

    }
}
