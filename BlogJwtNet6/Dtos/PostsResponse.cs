namespace BlogJwtNet6.Dtos
{
    public class PostsResponse : BaseResponse
    {
        public ICollection<PostResponse> Posts { get; set; }
    }
}
