using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_users_posts.Models;
using dotnet_users_posts.Services.PostService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_users_posts.Controllers
{
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
        public IActionResult Get()
        {
            return Ok(_postService.GetAllPosts());
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(int id)
        {
            return Ok(_postService.GetPostById(id));
        }

        [HttpPost]
        public IActionResult AddPost(Posts newPost)
        {
            return Ok(_postService.AddPost(newPost));
        }

    }
}