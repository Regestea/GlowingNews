using AWS.Shared.Client.Enums;
using AWS.Shared.Client.Extensions;
using AWS.Shared.Client.GrpcServices;
using EventBus.Messages.Events;
using GlowingNews.Application.Common.Interfaces;
using GlowingNews.Application.Common.Models;
using GlowingNews.Application.DTOs;
using IdentityServer.Shared.Client.Attributes;
using IdentityServer.Shared.Client.Repositories.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Security.Shared;

namespace GlowingNews.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private INewsRepository _newsRepository;
        private IJwtTokenRepository _jwtTokenRepository;
        private AwsGrpcService _awsGrpcService;
        private readonly IPublishEndpoint _publishEndpoint;

        public NewsController(INewsRepository newsRepository, IJwtTokenRepository jwtTokenRepository, AwsGrpcService awsGrpcService, IPublishEndpoint publishEndpoint)
        {
            _newsRepository = newsRepository;
            _jwtTokenRepository = jwtTokenRepository;
            _awsGrpcService = awsGrpcService;
            _publishEndpoint = publishEndpoint;
        }

        /// <summary>
        /// Get last user news
        /// </summary>
        /// <response code="200">Success: Single news</response>
        /// <response code="404">NotFound: User don't have any news</response>
        [ProducesResponseType(typeof(NewsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AuthorizeByIdentityServer(Roles.User + "|" + Roles.Admin)]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetLastUserNews(Guid userId)
        {
            var news = await _newsRepository.LastNewsAsync(userId);
            if (news != null)
            {
                return Ok(news);
            }

            return NotFound();
        }

        /// <summary>
        /// Get list of user News
        /// </summary>
        /// <response code="200">Success: List of news</response>
        /// <response code="404">NotFound: User don't have any news</response>
        [ProducesResponseType(typeof(List<NewsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AuthorizeByIdentityServer(Roles.User + "|" + Roles.Admin)]
        [HttpGet("{userId}/List")]
        public async Task<IActionResult> GetUserNewsList([FromRoute] Guid userId)
        {
            var newsList = await _newsRepository.NewsListAsync(userId);

            if (newsList != null)
            {
                

                return Ok(newsList);
            }

            return NotFound();
        }

        /// <summary>
        /// Add news
        /// </summary>
        /// <response code="200">Success: News id</response>
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [AuthorizeByIdentityServer(Roles.User + "|" + Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> AddNews(AddNewsModel addNewsModel)
        {
            var jwtToken = _jwtTokenRepository.GetJwtToken();
            var userDto = _jwtTokenRepository.ExtractUserDataFromToken(jwtToken);

            var addNewsDto = new AddNewsDto()
            {
                UserId = userDto.Id,
                Text = addNewsModel.Text,
                UserName = userDto.Name
            };

            if (addNewsModel.MediaToken != null && !string.IsNullOrEmpty(addNewsModel.MediaToken))
            {
                var newsFileDetail = await _awsGrpcService.GetObjectPathAsync(userDto.Id, addNewsModel.MediaToken);
                if (!string.IsNullOrEmpty(newsFileDetail.FilePath))
                {
                    addNewsDto.MediaPath = AwsFile.GetUrl(newsFileDetail.FilePath);
                    addNewsDto.MediaType = (MediaTypeDto)AwsFile.GetMediaType(newsFileDetail.FileFormat);
                }
            }

            var newsId = await _newsRepository.AddNewsAsync(addNewsDto);

            await _publishEndpoint.Publish<AddNewsToSearchIndexEvent>(new AddNewsToSearchIndexEvent
            {
                NewsId = newsId,
                Text = addNewsModel.Text
            });

            return Ok(newsId);
        }

        /// <summary>
        /// Edit news
        /// </summary>
        /// <response code="200">Success: News id</response>
        /// <response code="404">NotFound: news not found</response>
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AuthorizeByIdentityServer(Roles.User + "|" + Roles.Admin)]
        [HttpPatch("{newsId}")]
        public async Task<IActionResult> EditNews([FromRoute] Guid newsId, EditNewsModel editNewsModel)
        {
            var jwtToken = _jwtTokenRepository.GetJwtToken();
            var userDto = _jwtTokenRepository.ExtractUserDataFromToken(jwtToken);

            var editNewsDto = new EditNewsDto()
            {
                UserId = userDto.Id,
                NewsId = newsId,
                Text = editNewsModel.Text
            };

            var editNewsId = await _newsRepository.EditNewsAsync(editNewsDto);

            if (editNewsId != null)
            {
                await _publishEndpoint.Publish<UpdateNewsFromSearchIndexEvent>(new UpdateNewsFromSearchIndexEvent()
                {
                    NewsId = editNewsId.Value,
                    Text = editNewsModel.Text
                });
                return Ok(editNewsId);
            }

            return NotFound();
        }

        /// <summary>
        /// Upload user profile
        /// </summary>
        /// <response code="204">NoContent: news deleted</response>
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [AuthorizeByIdentityServer(Roles.User + "|" + Roles.Admin)]
        [HttpDelete("{newsId}")]
        public async Task<IActionResult> DeleteNews(Guid newsId)
        {
            var jwtToken = _jwtTokenRepository.GetJwtToken();
            var userDto = _jwtTokenRepository.ExtractUserDataFromToken(jwtToken);

            var imagePath = await _newsRepository.DeleteNewsAsync(userDto.Id, newsId);

            if (imagePath != null)
            {
                await _awsGrpcService.DeleteObjectAsync(userDto.Id, imagePath);
            }

            await _publishEndpoint.Publish<DeleteNewsFromSearchIndexEvent>(new DeleteNewsFromSearchIndexEvent()
            {
                NewsId = newsId
            });

            return NoContent();
        }
    }
}
