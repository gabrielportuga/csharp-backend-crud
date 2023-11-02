using KanbanBoard.Api.Utils.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OasTools.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IJwtToken _jwtToken;

        public AuthController(IJwtToken jwtToken)
        {
            _jwtToken = jwtToken;
        }

        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            if (login == "letscode" && password == "lets@123")
            {
                var token = _jwtToken.GenerateJwtToken(login);
                return Ok(new { token });
            }

            return Unauthorized();
        }
    }
}
