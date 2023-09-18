using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace JWT.Controllers
{
    [Route("")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        /// <summary>
        /// Возвращает токен к запрошенному скоупу
        /// </summary>
        /// <remarks>
        /// Логин и пароль захардкожены validUser validPassword
        ///  
        /// Сценарий использования, тут захардкожен client_credentials
        /// 
        /// Скоуп к которому запрашивается доступ, тут захардкожен jwtApi
        /// </remarks>
        [HttpPost("connect/token")]
        [Authorize]
        public IActionResult GetTokenByCredentials([FromForm(Name = "grant_type")] string grantType, [FromForm] string scope)
        {
            if(grantType != "client_credentials" ||  scope != "jwtApi")
            {
                return BadRequest();
            }

            var jwt = TokenService.GenerateToken(scope);

            return Ok(new AccessTokenDTO { AccessToken = jwt, TokenType = "Bearer", Scope = scope, expiresIn = 3600 });
        }


        public class AccessTokenDTO
        {
            [JsonPropertyName("access_token")]
            public string AccessToken { get; set; }
            [JsonPropertyName("expires_in")]
            public int expiresIn { get; set; }
            [JsonPropertyName("token_type")]
            public string TokenType { get; set; }
            [JsonPropertyName("scope")]
            public string Scope { get; set; }
        }

    }
}
