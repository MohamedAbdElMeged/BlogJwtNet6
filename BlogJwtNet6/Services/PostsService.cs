using BlogJwtNet6.Data;
using BlogJwtNet6.Dtos;
using BlogJwtNet6.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogJwtNet6.Services
{
    public class PostsService : IPostsService
    {
        private BlogDbContext _context;
        private readonly HttpContext? _httpContext;

        public PostsService(BlogDbContext context, IHttpContextAccessor httpContextAccessor)

        {
            _context = context;
            _httpContext = httpContextAccessor.HttpContext;
        }
        public async Task<CreatePostResponse> CreatePost(CreatePostRequest request)
        {
            var post = new Post()
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Body = request.Body,
                Author = (User) _httpContext.Items["User"],
                CreatedAt = DateTime.UtcNow,
            };
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return new CreatePostResponse()
            {
                Message= "Created Succussfully",
                Id = post.Id,
                Success = true,
                Title = post.Title,
                Body = post.Body,
                CreatedAt = (DateTime) post.CreatedAt
            };
        }

        public async Task<PostsResponse> GetAllPosts()
        {
            PostsResponse postsResponse = new PostsResponse();
            postsResponse.Message = "Get All Posts";
            postsResponse.Success = true;
            postsResponse.Posts = new List<PostResponse>();
            var posts = await _context.Posts.Include(p=>p.Author).ToListAsync();
            foreach (var post in posts)
            {
                var postResponse = new PostResponse
                {
                    Author = new UserResponse()
                    {
                        Email= post.Author.Email,
                        Id= post.Author.Id,
                        FirstName= post.Author.FirstName,
                        LastName = post.Author.LastName
                    },
                    Title = post.Title,
                    Body = post.Body,
                    CreatedAt = (DateTime) post.CreatedAt,
                    Id= post.Id

                };

                postsResponse.Posts.Add(postResponse);
            }
            return postsResponse;
        }
    }
}
