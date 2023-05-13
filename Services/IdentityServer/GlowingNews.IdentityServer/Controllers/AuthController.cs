using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GlowingNews.IdentityServer.Context;
using GlowingNews.IdentityServer.Entities;
using GlowingNews.IdentityServer.Extensions;
using GlowingNews.IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Security.Shared;
using Microsoft.Win32;

namespace GlowingNews.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IdentityServerContext _identityServerContext;

        public AuthController(IdentityServerContext identityServerContext)
        {
            _identityServerContext = identityServerContext;
        }


        [HttpPost("/Register")]
        public async Task<IActionResult> Register(RegisterModel register)
        {
            register.Email = register.Email.Trim().ToLower();
            register.UserName = register.UserName.Trim();

            var existUserName = await _identityServerContext.Users.AnyAsync(x => x.Name == register.UserName);
            if (existUserName)
            {
                ModelState.AddModelError(nameof(register.UserName), "this user name is token");
                return BadRequest(ModelState);
            }

            var existEmail = await _identityServerContext.Users.AnyAsync(x => x.Email == register.Email);
            if (existEmail)
            {
                ModelState.AddModelError(nameof(register.Email), "this email registered");
                return BadRequest(ModelState);
            }

            var userRole = await _identityServerContext.Roles.SingleAsync(x => x.RoleTitle == Roles.User);

            var user = new User()
            {
                Id = Guid.NewGuid(),
                Email = register.Email,
                Name = register.UserName,
                Password = PasswordHash.Hash(register.Password)
            };

            await _identityServerContext.Users.AddAsync(user);

            await _identityServerContext.UserRoles.AddAsync(new UserRole()
                { Id = Guid.NewGuid(), RoleId = userRole.Id, UserId = user.Id });

            await _identityServerContext.SaveChangesAsync();

            return Ok(user.Id);
        }

        [HttpPost("/Login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            login.Email = login.Email.Trim().ToLower();
            var user = _identityServerContext.Users.FirstOrDefault(x =>
                x.Email == login.Email && x.Password == PasswordHash.Hash(login.Password));

            if (user == null)
            {
                ModelState.AddModelError(nameof(login.Email), "email not found");
                return NotFound(ModelState);
            }

            var userRoles = await _identityServerContext.UserRoles.Where(x => x.UserId == user.Id).Include(x => x.Role)
                .ToListAsync();


            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration.GetValue<string>("JWT:SecretKey") ??
                throw new ArgumentNullException(nameof(configuration))));

            string hostUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);

            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            foreach (var userRole in userRoles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, userRole.Role.RoleTitle));
            }

            var tokenOption = new JwtSecurityToken(
                issuer: hostUrl,
                claims: userClaims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOption);


            return Ok(new { Token = tokenString });
        }
    }
}