using IdentityServer.Shared.Client.Attributes;
using IdentityServer.Shared.Client.Repositories;
using IdentityServer.Shared.Client.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Security.Shared;
using UserAccount.Application.Common.Interfaces;
using UserAccount.Application.Common.Models;
using UserAccount.Application.DTOs;
using UserAccount.Infrastructure.Persistence;

namespace UserAccount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenRepository _jwtTokenRepository;

        public UserController(IUserRepository userRepository, IJwtTokenRepository jwtTokenRepository)
        {
            _userRepository = userRepository;
            _jwtTokenRepository = jwtTokenRepository;
        }

        /// <summary>
        /// Get logged in user
        /// </summary>
        /// <response code="200">Success: UserDetail</response>
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [AuthorizeByIdentityServer(Roles.User + "|" + Roles.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var jwtToken = _jwtTokenRepository.GetJwtToken();
            var userDto = _jwtTokenRepository.ExtractUserDataFromToken(jwtToken);

            var user = await _userRepository.GetUserAsync(userDto.Id);

            if (user == null)
            {
                await _userRepository.CreateUserAsync(new CreateUserDto()
                {
                    Id = userDto.Id,
                    Name = userDto.Name,
                    Email = userDto.Email
                });
                user = await _userRepository.GetUserAsync(userDto.Id);
            }
            return Ok(user);
        }


        /// <summary>
        /// Get User profile By Id
        /// </summary>
        /// <response code="200">Success: User profile</response>
        /// <response code="404">Success: User not found</response>
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [AuthorizeByIdentityServer(Roles.User + "|" + Roles.Admin)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user=await _userRepository.GetUserAsync(id);
            if (user==null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// edit logged in user
        /// </summary>
        /// <response code="204">Success: User edited </response>
        [ProducesResponseType( StatusCodes.Status204NoContent)]
        [AuthorizeByIdentityServer(Roles.User + "|" + Roles.Admin)]
        [HttpPatch]
        public async Task<IActionResult> EditUserDetail([FromBody] EditUserModel editUserModel)
        {
            var jwtToken = _jwtTokenRepository.GetJwtToken();
            var userDto = _jwtTokenRepository.ExtractUserDataFromToken(jwtToken);

            await _userRepository.EditUserAsync(userDto.Id, editUserModel);

            return NoContent();
        }
    }
}
