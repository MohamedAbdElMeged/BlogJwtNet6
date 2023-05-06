using BlogJwtNet6.Models;

namespace BlogJwtNet6.Dtos
{
    public class RegisterResponse
    {
        public string? Message { get; set; }
        public Guid? Id { get; set; }

        public string? AuthToken { get; set; }

    }
}
