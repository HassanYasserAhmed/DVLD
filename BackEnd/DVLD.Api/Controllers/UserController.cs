using DVLD.BusinessLogicLayer;
using DVLD.DataAccessLayer.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Api.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            _userService = new UserService(connectionString);
        }
        [HttpPost("Login")]
        public async Task<ActionResult>  LoginAsync(LoginRequestDTO request)
        {
            return Ok("test");
        }
    }
}
