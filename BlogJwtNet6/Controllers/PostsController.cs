using BlogJwtNet6.Authorization;
using BlogJwtNet6.Data;
using BlogJwtNet6.Dtos;
using BlogJwtNet6.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogJwtNet6.Controllers
{
    [HotAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postsService;

        public PostsController(IPostsService postsService)
        {
            _postsService = postsService;
        }
        [HttpPost]
        [Route("create_post")]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest createPostRequest)
        {
            if (createPostRequest == null)
            {
                return UnprocessableEntity("can't be null");
            } 
            var response = await _postsService.CreatePost(createPostRequest);
            return Created($"/users/#{response.Id}", response);


        }

        [HttpGet]
        [BlogAllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var response = await _postsService.GetAllPosts();
            return Ok(response);
        }
    }
}
