using BookLendingAPI.Dtos;
using BookLendingAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookLendingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AuthDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegistrUserIdentity model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authDto = await _userService.RegisterUserAsync(model);
            return MapResponseToActionResult(authDto);
        }


        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var authDto = await _userService.LoginAsync(login);
            return MapResponseToActionResult(authDto);
        }
    }
}
