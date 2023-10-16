using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PersonalSiteApi.EntityFramework;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace PersonalSiteApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {
        public AuthController(IConfiguration config, PersonalSiteContext context) : base(config, context) { }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JWT))]
        public IActionResult Login(User user)
        {
            if (user.Username != "woutvanriel" || user.Password != _config.GetValue<string>("password"))
            {
                return BadRequest("Username or Password are invalid.");
            }
            return Ok(new JWT { jwt = GenerateToken() });
        }

        [HttpGet]
        [Authorize]
        public bool CanActivate()
        {
            return true;
        }

        private string GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("JwtSettings:Key")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config.GetValue<string>("JwtSettings:Issuer"),
                null,
                null,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class User
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class JWT
    {
        public string? jwt { get; set; }
    }
}
