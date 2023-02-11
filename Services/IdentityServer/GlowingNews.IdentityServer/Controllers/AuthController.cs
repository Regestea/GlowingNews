using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GlowingNews.IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;

namespace GlowingNews.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ValidateToken()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("f7e4d3d7-3045-4ae6-b9f0-a4a4670e60d7"));

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);

            var tokenOption = new JwtSecurityToken(
                issuer: "https://localhost:7126",
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Name,model.UserName),
                    new Claim(ClaimTypes.Role,"Admin")
                },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOption);

            return Ok(new { token = tokenString });
        }
    }
}
