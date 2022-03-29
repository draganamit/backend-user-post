using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend_user_post.Data;
using backend_user_post.Dtos.Post;
using backend_user_post.Models;
using dotnet_users_posts.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_users_posts.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public PostService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<GetPostDto>>> AddPost(AddPostDto newPost)
        {
            ServiceResponse<List<GetPostDto>> serviceResponse = new ServiceResponse<List<GetPostDto>>();
            Posts post = _mapper.Map<Posts>(newPost);
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            serviceResponse.Data = (_context.Posts.Select(c => _mapper.Map<GetPostDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPostDto>>> DeletePost(int id)
        {
            ServiceResponse<List<GetPostDto>> serviceResponse = new ServiceResponse<List<GetPostDto>>();
            try
            {
                Posts post = _context.Posts.First(c => c.Id == id);
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();

                serviceResponse.Data = (_context.Posts.Select(c => _mapper.Map<GetPostDto>(c))).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPostDto>>> GetAllPosts(int userId)
        {
            ServiceResponse<List<GetPostDto>> serviceResponse = new ServiceResponse<List<GetPostDto>>();
            List<Posts> dbPosts = await _context.Posts.Where(c => c.User.Id == userId).ToListAsync();
            serviceResponse.Data = (dbPosts.Select(c => _mapper.Map<GetPostDto>(c))).ToList();
            // serviceResponse.Data = _mapper.Map<List<GetPostDto>>(posts).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPostDto>> GetPostById(int id)
        {
            ServiceResponse<GetPostDto> serviceResponse = new ServiceResponse<GetPostDto>();
            Posts dbPost = await _context.Posts.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetPostDto>(dbPost);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPostDto>> UpdatePost(UpdatePostDto updatedPost)
        {
            ServiceResponse<GetPostDto> serviceResponse = new ServiceResponse<GetPostDto>();
            try
            {
                Posts post = await _context.Posts.FirstOrDefaultAsync(c => c.Id == updatedPost.Id);
                post.Text = updatedPost.Text;

                _context.Posts.Update(post);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetPostDto>(post);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;

        }
    }
}