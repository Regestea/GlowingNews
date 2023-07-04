using IdentityServer.Shared.Client.Attributes;
using IdentityServer.Shared.Client.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Security.Shared;
using UserAccount.Application.Common.Interfaces;

namespace UserAccount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IFollowRepository _followRepository;
        private readonly IJwtTokenRepository _jwtTokenRepository;

        public FollowController(IFollowRepository followRepository, IJwtTokenRepository jwtTokenRepository)
        {
            _followRepository = followRepository;
            _jwtTokenRepository = jwtTokenRepository;
        }

        [AuthorizeByIdentityServer(Roles.User + "|" + Roles.Admin)]
        [HttpGet("{userId}/FollowingList")]
        public async Task<IActionResult> FollowingList([FromRoute] Guid userId)
        {
            var followingList = await _followRepository.FollowingListAsync(userId);

            return Ok(followingList);
        }

        [AuthorizeByIdentityServer(Roles.User + "|" + Roles.Admin)]
        [HttpGet("{userId}/FollowerList")]
        public async Task<IActionResult> FollowerList([FromRoute] Guid userId)
        {
            var followerList = await _followRepository.FollowerListAsync(userId);

            return Ok(followerList);
        }

        [AuthorizeByIdentityServer(Roles.User + "|" + Roles.Admin)]
        [HttpPost("{followingId}")]
        public async Task<IActionResult> Follow([FromRoute] Guid followingId)
        {
            var jwtToken = _jwtTokenRepository.GetJwtToken();
            var userDto = _jwtTokenRepository.ExtractUserDataFromToken(jwtToken);
            await _followRepository.FollowAsync(userDto.Id, followingId);
            return NoContent();
        }

        [AuthorizeByIdentityServer(Roles.User + "|" + Roles.Admin)]
        [HttpDelete("{unFollowId}")]
        public async Task<IActionResult> UnFollow([FromRoute] Guid unFollowId)
        {
            var jwtToken = _jwtTokenRepository.GetJwtToken();
            var userDto = _jwtTokenRepository.ExtractUserDataFromToken(jwtToken);

            await _followRepository.UnFollowAsync(userDto.Id, unFollowId);
            return NoContent();
        }

        [AuthorizeByIdentityServer(Roles.User + "|" + Roles.Admin)]
        [HttpGet("IsFollowed/{userId}")]
        public async Task<IActionResult> IsFollowed([FromRoute] Guid userId)
        {
            var jwtToken = _jwtTokenRepository.GetJwtToken();
            var userDto = _jwtTokenRepository.ExtractUserDataFromToken(jwtToken);

            bool isFollowed = await _followRepository.IsFollowedAsync(userDto.Id, userId);

            return Ok(isFollowed);
        }
    }
}
