using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend_user_post.Dtos.Post;
using backend_user_post.Models;
using dotnet_users_posts.Models;

namespace dotnet_users_posts.Services.PostService
{
    public class PostService : IPostService
    {
        private static List<Posts> posts = new List<Posts>{
            new Posts(),
            new Posts{Id=1, Text="The book is very interesting."}
        };
        private readonly IMapper _mapper;
        public PostService(IMapper mapper)
        {
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<GetPostDto>>> AddPost(AddPostDto newPost)
        {
            ServiceResponse<List<GetPostDto>> serviceResponse = new ServiceResponse<List<GetPostDto>>();
            Posts post = _mapper.Map<Posts>(newPost);
            post.Id = posts.Max(c => c.Id) + 1;
            posts.Add(post);
            serviceResponse.Data = (posts.Select(c => _mapper.Map<GetPostDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPostDto>>> DeletePost(int id)
        {
            ServiceResponse<List<GetPostDto>> serviceResponse = new ServiceResponse<List<GetPostDto>>();
            try
            {
            Posts post = posts.First(c=>c.Id==id);
            posts.Remove(post);

            serviceResponse.Data =(posts.Select(c => _mapper.Map<GetPostDto>(c))).ToList();
            }
            catch(Exception ex)
            {
                serviceResponse.Success=false;
                serviceResponse.Message=ex.Message;                
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPostDto>>> GetAllPosts()
        {
            ServiceResponse<List<GetPostDto>> serviceResponse = new ServiceResponse<List<GetPostDto>>();
            serviceResponse.Data = (posts.Select(c => _mapper.Map<GetPostDto>(c))).ToList();
            // serviceResponse.Data = _mapper.Map<List<GetPostDto>>(posts).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPostDto>> GetPostById(int id)
        {
            ServiceResponse<GetPostDto> serviceResponse = new ServiceResponse<GetPostDto>();
            serviceResponse.Data = _mapper.Map<GetPostDto>(posts.FirstOrDefault(c => c.Id == id));
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPostDto>> UpdatePost(UpdatePostDto updatedPost)
        {
            ServiceResponse<GetPostDto> serviceResponse = new ServiceResponse<GetPostDto>();
            try
            {
            Posts post = posts.FirstOrDefault(c=>c.Id == updatedPost.Id);
            post.Text = updatedPost.Text;

            serviceResponse.Data = _mapper.Map<GetPostDto>(post);
            }
            catch(Exception ex)
            {
                serviceResponse.Success=false;
                serviceResponse.Message=ex.Message;                
            }

            return serviceResponse;

        }
    }
}