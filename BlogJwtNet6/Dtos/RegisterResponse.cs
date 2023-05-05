using BlogJwtNet6.Models;

namespace BlogJwtNet6.Dtos
{
    public class RegisterResponse
    {
        public UserResponse User { get; set; }
        public string AuthToken { get; set; }

    }
}
