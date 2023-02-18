using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GlowingNews.IdentityServer.Entities;
using GlowingNews.IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GlowingNews.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public Task<IActionResult> Login(LoginModel model)
        {
            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWT:SecretKey") ?? throw new ArgumentNullException(nameof(configuration))));

            string hostUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);

            var tokenOption = new JwtSecurityToken(
                issuer: hostUrl,
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Name,model.UserName),
                    new Claim(ClaimTypes.Role,Roles.Admin.ToString()),
                    new Claim(ClaimTypes.Role,Roles.User.ToString())
                },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOption);
                

            return Task.FromResult<IActionResult>(Ok(new { token = tokenString }));
        }
    }
}
