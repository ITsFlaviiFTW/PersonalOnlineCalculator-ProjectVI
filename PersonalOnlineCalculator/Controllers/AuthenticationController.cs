using Microsoft.AspNetCore.Mvc;
using PersonalOnlineCalculator.Models;
using PersonalOnlineCalculator.Services;
using System.Threading.Tasks;

namespace PersonalOnlineCalculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller //Changed from Controllerbase to Controller
    {
        private readonly AuthenticationService _authService;

        public AuthenticationController(AuthenticationService authService)
        {
            _authService = authService;
        }
        //Added this to fix routing issues
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View("~/Views/Home/Login.cshtml");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            try
            {
                // Create a new User object from the RegisterModel
                var newUser = new User
                {
                    Email = registerModel.Email,
                    Username = registerModel.Username,
                    PasswordHash = registerModel.Password // Make sure to hash the password
                };

                // Call the service to handle registration
                var registeredUser = await _authService.RegisterUser(newUser);
                return Ok(registeredUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                var user = await _authService.LoginUser(loginModel.Username, loginModel.Password);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserProfileAsync(int id)
        {
            var user = _authService.GetUserById(id);
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
        public IActionResult UpdateUserProfile(int id, [FromBody] User user)
        {
            try
            {
                user.Id = id; // Ensure the ID is set correctly
                _authService.UpdateUserProfile(user);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}/password")]
        public IActionResult UpdateUserPassword(int id, [FromBody] string newPassword)
        {
            try
            {
                _authService.UpdateUserPassword(id, newPassword);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
