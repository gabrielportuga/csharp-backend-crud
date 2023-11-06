using KanbanBoard.Api.Utils.Configurations;
using KanbanBoard.Api.Utils.Security;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBoard.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly IJwtToken _jwtToken;

        public LoginController(IJwtToken jwtToken)
        {
            _jwtToken = jwtToken;
        }

        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            if (login == KanbanBoardConfig.Login && password == KanbanBoardConfig.Password)
            {
                var token = _jwtToken.GenerateJwtToken(login);
                return Ok(new { token });
            }

            return Unauthorized();
        }
    }
}
