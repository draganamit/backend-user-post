using AutoMapper;
using backend_user_post.Dtos.Post;
using dotnet_users_posts.Models;

namespace backend_user_post
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Posts, GetPostDto>();
            CreateMap<AddPostDto, Posts>();
        }
    }
}