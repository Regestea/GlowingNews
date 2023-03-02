using IdentityServer.Shared.Client.Attributes;
using IdentityServer.Shared.Client.Repositories;
using IdentityServer.Shared.Client.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Security.Shared;
using UserAccount.Application.Common.Interfaces;
using UserAccount.Application.Common.Models;
using UserAccount.Infrastructure.Persistence;

namespace UserAccount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;
        private UserAccountContext _userAccountContext;
        private IJwtTokenRepository _jwtTokenRepository;

        public UserController(IUserRepository userRepository, UserAccountContext userAccountContext, IJwtTokenRepository jwtTokenRepository)
        {
            _userRepository = userRepository;
            _userAccountContext = userAccountContext;
            _jwtTokenRepository = jwtTokenRepository;
        }


        [HttpGet]
        [AuthorizeByIdentityServer(Roles.User+","+Roles.Admin)]
        public async Task<IActionResult> Get()
        {
            var jwtToken = _jwtTokenRepository.GetJwtToken();
            var userDto = _jwtTokenRepository.ExtractUserDataFromToken(jwtToken);

            var user = await _userRepository.GetUserAsync(userDto.Id);

            if (user == null)
            {

                await _userRepository.CreateUserAsync(new CreateUserModel()
                {
                    Id = userDto.Id,
                    Name = userDto.Name,
                    Email = userDto.Email
                });
                user = await _userRepository.GetUserAsync(userDto.Id);
            }

            return Ok(user);
        }
    }
}
