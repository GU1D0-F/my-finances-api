using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinances.Users;

namespace MyFinances.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) =>
            _userService = userService;


        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterModel userModel)
        {
            var result = await _userService.RegisterAsync(userModel);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromQuery] string email, [FromQuery] string password) =>
            Ok(await _userService.LoginAsync(email, password));


        [HttpGet, Authorize]
        public IActionResult TesteAuthorize() =>
            Ok("Teste Authorize Ok");
    }
}
