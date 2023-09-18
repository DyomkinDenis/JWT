using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace JWT.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetToken()
        {

            var jwt = TokenService.GenerateToken();
            return Ok(new { Token = jwt });

        }
    }
}
