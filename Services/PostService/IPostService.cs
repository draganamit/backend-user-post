using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_users_posts.Services.PostService
{
    public interface IPostService
    {
        List<Posts> GetAllPosts();

        Posts GetPostById(int id);

        List<Posts> AddPost(Posts newPost);
    }
}