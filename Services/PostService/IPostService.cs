using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_user_post.Dtos.Post;
using backend_user_post.Models;
using dotnet_users_posts.Models;

namespace dotnet_users_posts.Services.PostService
{
    public interface IPostService
    {
        Task<ServiceResponse<List<GetPostDto>>> GetAllPosts(int userId);

        Task<ServiceResponse<GetPostDto>> GetPostById(int id);

        Task<ServiceResponse<List<GetPostDto>>> AddPost(AddPostDto newPost);

        Task<ServiceResponse<GetPostDto>> UpdatePost(UpdatePostDto updatedPost);

        Task<ServiceResponse<List<GetPostDto>>> DeletePost(int id);
    }
}