using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_users_posts.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_users_posts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {

        private static List<Posts> posts = new List<Posts>{
            new Posts(),
            new Posts{Id=1, Text="The book is very interesting."}
        };

        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(int id)
        {
            return Ok(posts.FirstOrDefault(c => c.Id == id));
        }
        
        [HttpPost]
        public IActionResult AddPost(Posts newPost)
        {
            posts.Add(newPost);
            return Ok(posts);            
        }
    
    }
}