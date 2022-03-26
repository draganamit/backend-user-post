using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_users_posts.Models;

namespace dotnet_users_posts.Services.PostService
{
    public class PostService : IPostService
    {
        private static List<Posts> posts = new List<Posts>{
            new Posts(),
            new Posts{Id=1, Text="The book is very interesting."}
        };
        public List<Posts> AddPost(Posts newPost)
        {
            posts.Add(newPost);
            return posts;
        }

        public List<Posts> GetAllPosts()
        {
            return posts;
        }

        public Posts GetPostById(int id)
        {
            return posts.FirstOrDefault(c => c.Id == id);
        }
    }
}