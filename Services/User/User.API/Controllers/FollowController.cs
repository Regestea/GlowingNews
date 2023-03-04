using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAccount.Application.Common.Interfaces;

namespace UserAccount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private IFollowRepository _followRepository;

        public FollowController(IFollowRepository followRepository)
        {
            _followRepository = followRepository;
        }

        [HttpGet("{userId}/FollowingList")]
        public async Task<IActionResult> FollowingList([FromRoute]Guid userId)
        {
            return Ok();
        }

        //user can search and pageing
        [HttpGet("{userId}/FollowerList")]
        public async Task<IActionResult> FollowerList([FromRoute] Guid userId)
        {
            return Ok();
        }

        [HttpPost("{followingId}")]
        public async Task<IActionResult> Follow([FromRoute]Guid followingId)
        {
            return Ok();
        }

        [HttpDelete("{unFollowId}")]
        public async Task<IActionResult> UnFollow([FromRoute]Guid unFollowId)
        {
            return Ok();
        }
    }
}
