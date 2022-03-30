using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using backend_user_post.Dtos.Post;
using backend_user_post.Models;
using dotnet_users_posts.Models;
using dotnet_users_posts.Services.PostService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_users_posts.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {

        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;

        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _postService.GetAllPosts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _postService.GetPostById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostDto newPost)
        {
            return Ok(await _postService.AddPost(newPost));
        }

        [HttpPut]    
        public async Task<IActionResult> UpdatePost(UpdatePostDto updatedPost)
        {
            ServiceResponse<GetPostDto> response = await _postService.UpdatePost(updatedPost);
            if(response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse<List<GetPostDto>> response = await _postService.DeletePost(id);
            if(response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

    }
}