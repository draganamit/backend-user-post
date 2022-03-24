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

        private static Posts post1 = new Posts();

        public IActionResult Get()
        {
            return Ok(post1);
        }

        
    }
}