using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using backend_user_post.Data;
using backend_user_post.Dtos.Post;
using backend_user_post.Models;
using dotnet_users_posts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace dotnet_users_posts.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PostService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;

        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<List<GetPostDto>>> AddPost(AddPostDto newPost)
        {
            ServiceResponse<List<GetPostDto>> serviceResponse = new ServiceResponse<List<GetPostDto>>();
            Posts post = _mapper.Map<Posts>(newPost);
            post.User =await _context.User.FirstOrDefaultAsync( u => u.Id == GetUserId());
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            serviceResponse.Data = (_context.Posts.Where(c => c.User.Id == GetUserId()).Select(c => _mapper.Map<GetPostDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPostDto>>> DeletePost(int id)
        {
            ServiceResponse<List<GetPostDto>> serviceResponse = new ServiceResponse<List<GetPostDto>>();
            try
            {
                Posts post =await _context.Posts.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
                if(post != null)
                {
                    _context.Posts.Remove(post);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = (_context.Posts.Where(c => c.User.Id == GetUserId()).Select(c => _mapper.Map<GetPostDto>(c))).ToList();
                }
                else
                {
                    serviceResponse.Success=false;
                    serviceResponse.Message="Post not found.";
                }
                
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPostDto>>> GetAllPosts()
        {
            ServiceResponse<List<GetPostDto>> serviceResponse = new ServiceResponse<List<GetPostDto>>();
            List<Posts> dbPosts = await _context.Posts.Where(c => c.User.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = (dbPosts.Select(c => _mapper.Map<GetPostDto>(c))).ToList();
            // serviceResponse.Data = _mapper.Map<List<GetPostDto>>(posts).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPostDto>> GetPostById(int id)
        {
            ServiceResponse<GetPostDto> serviceResponse = new ServiceResponse<GetPostDto>();
            Posts dbPost = await _context.Posts.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<GetPostDto>(dbPost);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPostDto>> UpdatePost(UpdatePostDto updatedPost)
        {
            ServiceResponse<GetPostDto> serviceResponse = new ServiceResponse<GetPostDto>();
            try
            {
                Posts post = await _context.Posts.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == updatedPost.Id);
                if(post.User.Id == GetUserId())
                {
                    post.Text = updatedPost.Text;

                    _context.Posts.Update(post);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetPostDto>(post);
                }
                else
                {
                    serviceResponse.Success=false;
                    serviceResponse.Message="Post not found";
                }
                
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