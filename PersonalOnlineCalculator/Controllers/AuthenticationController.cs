using Microsoft.AspNetCore.Mvc;
using PersonalOnlineCalculator.Models;
using PersonalOnlineCalculator.Services;
using System.Threading.Tasks;

namespace PersonalOnlineCalculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authService;

        public AuthenticationController(AuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User newUser)
        {
            try
            {
                var user = await _authService.RegisterUser(newUser);
                return Ok(user);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User loginDetails)
        {
            try
            {
                var user = await _authService.LoginUser(loginDetails.Username, loginDetails.PasswordHash);
                return Ok(user);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserProfile(int id)
        {
            var user = await _authService.GetUserById(id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("User not found.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProfile(int id, [FromBody] User user)
        {
            try
            {
                user.Id = id; // Ensure the ID is set correctly
                await _authService.UpdateUserProfile(user);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}/password")]
        public async Task<IActionResult> UpdateUserPassword(int id, [FromBody] string newPassword)
        {
            try
            {
                await _authService.UpdateUserPassword(id, newPassword);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
