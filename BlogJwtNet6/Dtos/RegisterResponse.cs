using BlogJwtNet6.Models;

namespace BlogJwtNet6.Dtos
{
    public class RegisterResponse : BaseResponse
    {
        public Guid? Id { get; set; }


        public string? AuthToken { get; set; }

    }
}
