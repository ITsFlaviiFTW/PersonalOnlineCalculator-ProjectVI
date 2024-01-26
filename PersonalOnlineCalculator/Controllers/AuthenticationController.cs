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

    }
}
