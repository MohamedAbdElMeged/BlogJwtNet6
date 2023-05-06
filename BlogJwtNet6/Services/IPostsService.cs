using BlogJwtNet6.Dtos;

namespace BlogJwtNet6.Services
{
    public interface IPostsService
    {
        public Task<CreatePostResponse> CreatePost(CreatePostRequest request);

        public Task<PostsResponse> GetAllPosts();

    }
}
