using CarsCatalog.Api.Args;
using CarsCatalog.Api.Managers;
using CarsCatalog.Api.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarsCatalog.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]AuthenticationArgs user)
        {
            if (user is null)
            {
                return BadRequest("Invalid client request");
            }
            if (_authManager.AuthenticateUser(user))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@NetBet"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:6055",
                    audience: "https://localhost:6055",
                    claims: new List<Claim>() {new Claim(ClaimTypes.Role, "Administrator") },
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new AuthenticatedResponse { Token = tokenString });
            }
            return Unauthorized();
        }
    }
}
