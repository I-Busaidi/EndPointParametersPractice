using EndpointParametersSolution.DTOs;
using EndpointParametersSolution.Services;
using Microsoft.AspNetCore.Mvc;

namespace EndpointParametersSolution.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Add-User")]
        public IActionResult AddUser([FromBody] UserInputDTO userInputDTO)
        {
            try
            {
                var user = _userService.AddUser(userInputDTO);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
    }
}
