using DAL;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace API_EPSI.Controllers
{
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin _repo;
        private readonly IConfiguration _config;

        public LoginController(ILogin repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous, HttpPost("Login")]
        public async Task<ActionResult> Login(User user)
        {
            ActionResult response = Unauthorized();
            var okUser = await _repo.Login(user);
            if (okUser)
            {
                var token = GenerateToken(user);
                response = Ok(new { token = token });
            }

            return response;
        }

        [AllowAnonymous, HttpPost("Register")]
        public async Task<ActionResult> Register(User user)
        {
            await _repo.Register(user);
            var token = GenerateToken(user);
            return Ok(new { token = token });
        }
    }
}
