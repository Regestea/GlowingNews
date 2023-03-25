using IdentityServer.Shared.Client.Attributes;
using IdentityServer.Shared.Client.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsLike.Application.DTOs;
using NewsLike.Application.Features.Likes.Commands.CreateLike;
using NewsLike.Application.Features.Likes.Commands.DeleteLike;
using NewsLike.Application.Features.Likes.Queries.IsLiked;
using NewsLike.Application.Features.Likes.Queries.LikedNewsList;
using NewsLike.Application.Features.Likes.Queries.NewsLikeCount;
using Security.Shared;

namespace NewsLike.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private IMediator _mediator;
        private IJwtTokenRepository _jwtTokenRepository;

        public LikeController(IMediator mediator, IJwtTokenRepository jwtTokenRepository)
        {
            _mediator = mediator;
            _jwtTokenRepository = jwtTokenRepository;
        }


        /// <summary>
        /// Like a news
        /// </summary>
        /// <response code="204">NoContent</response>
        /// <response code="400">BadRequest: the news already liked by this user</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AuthorizeByIdentityServer(Roles.User + "|" + Roles.Admin)]
        [HttpPost("{newsId}")]
        public async Task<IActionResult> LikeNews([FromRoute] Guid newsId)
        {
            var jwtToken = _jwtTokenRepository.GetJwtToken();
            var userDto = _jwtTokenRepository.ExtractUserDataFromToken(jwtToken);

            var isLikedAlready = await _mediator.Send(new CreateLikeCommand() { UserId = userDto.Id, NewsId = newsId });
            if (isLikedAlready == false)
            {
                await _mediator.Send(new CreateLikeCommand() { UserId = userDto.Id, NewsId = newsId });

                return NoContent();
            }

            return BadRequest("the news already liked by this user");
        }


        /// <summary>
        /// Get news like count
        /// </summary>
        /// <response code="200">Success: Like Count</response>
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [AuthorizeByIdentityServer(Roles.User + "|" + Roles.Admin)]
        [HttpGet("{newsId}/Count")]
        public async Task<IActionResult> NewsLikeCount([FromRoute] Guid newsId)
        {
            var likeCount = await _mediator.Send(new NewsLikeCountQuery() { NewsId = newsId });

            return Ok(likeCount);
        }


        /// <summary>
        /// Is liked news
        /// </summary>
        /// <response code="200">Success: Is liked news</response>
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [AuthorizeByIdentityServer(Roles.User + "|" + Roles.Admin)]
        [HttpGet("{newsId}")]
        public async Task<IActionResult> IsLikedNews([FromRoute] Guid newsId)
        {
            var jwtToken = _jwtTokenRepository.GetJwtToken();
            var userDto = _jwtTokenRepository.ExtractUserDataFromToken(jwtToken);

            var isLiked = await _mediator.Send(new IsLikedQuery() { UserId = userDto.Id, NewsId = newsId });

            return Ok(isLiked);
        }


        /// <summary>
        /// Get User liked news list
        /// </summary>
        /// <response code="200">Success: User liked news list</response>
        [ProducesResponseType(typeof(List<LikeDto>), StatusCodes.Status200OK)]
        [AuthorizeByIdentityServer(Roles.User + "|" + Roles.Admin)]
        [HttpGet("/UserLikes")]
        public async Task<IActionResult> UserLikedNewsList()
        {
            var jwtToken = _jwtTokenRepository.GetJwtToken();
            var userDto = _jwtTokenRepository.ExtractUserDataFromToken(jwtToken);

            var likedList = await _mediator.Send(new LikedNewsListQuery() { UserId = userDto.Id });

            return Ok(likedList);
        }


        /// <summary>
        /// Remove news like
        /// </summary>
        /// <response code="204">NoContent</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [AuthorizeByIdentityServer(Roles.User + "|" + Roles.Admin)]
        [HttpDelete("{newsId}")]
        public async Task<IActionResult> RemoveLike([FromRoute] Guid newsId)
        {
            var jwtToken = _jwtTokenRepository.GetJwtToken();
            var userDto = _jwtTokenRepository.ExtractUserDataFromToken(jwtToken);

            await _mediator.Send(new DeleteLikeCommand() { UserId = userDto.Id, NewsId = newsId });

            return NoContent();
        }
    }
}