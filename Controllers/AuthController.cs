using System.Threading.Tasks;
using backend_user_post.Data;
using backend_user_post.Dtos.User;
using backend_user_post.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend_user_post.Controllers
{
    [ApiController]
    [Route("controller")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            ServiceResponse<int> response = await _authRepo.Register(
                new User { Username = request.Username }, request.Password
            );
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}