using IdentityServer.Shared.Client.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.Shared.Client.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace IdentityServer.Shared.Client.Repositories
{
    public class JwtTokenRepository: IJwtTokenRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtTokenRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserDto ExtractUserDataFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userName = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var email = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            var user = new UserDto()
            {
                Id = Guid.Parse(userId!),Name = userName!,Email = email!
            };

            return user;
        }

        public string GetJwtToken()
        {
            _httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("Authorization", out var authorizationHeader);
            var jwtToken = authorizationHeader.ToString().Replace("Bearer ", "");
            return jwtToken;
        }
    }
}
