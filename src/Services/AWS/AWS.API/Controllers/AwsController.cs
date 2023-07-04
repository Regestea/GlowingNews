using Amazon.S3;
using AWS.Application.Common.Interfaces;
using AWS.Application.DTOs;
using AWS.Application.Models;
using AWS.Domain.Entities;
using AWS.Domain.Enums;
using AWS.Infrastructure.Persistence.AWS;
using IdentityServer.Shared.Client.Attributes;
using IdentityServer.Shared.Client.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Security.Shared;
using Security.Shared.Extensions;

namespace AWS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwsController : ControllerBase
    {
        private IAwsFileRepository _awsFileRepository;
        private IJwtTokenRepository _jwtTokenRepository;
        private AwsIndexDbContext _awsIndexDbContext;

        public AwsController(IAwsFileRepository awsFileRepository, IJwtTokenRepository jwtTokenRepository, AwsIndexDbContext awsIndexDbContext)
        {
            _awsFileRepository = awsFileRepository;
            _jwtTokenRepository = jwtTokenRepository;
            _awsIndexDbContext = awsIndexDbContext;
        }

        /// <summary>
        /// Upload user profile
        /// </summary>
        /// <response code="200">Success: Uploaded file token</response>
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [HttpPost("UserProfileImage")]
        [AuthorizeByIdentityServer(Roles.User+"|"+Roles.Admin)]
        public async Task<IActionResult> UserProfileImage([FromForm] ImageUploadModel imageUploadModel)
        {
            var jwtToken = _jwtTokenRepository.GetJwtToken();
            var userDto = _jwtTokenRepository.ExtractUserDataFromToken(jwtToken);

            string imageName=await _awsFileRepository.UploadFileAsync(Bucket.profile, imageUploadModel.Image, S3CannedACL.PublicRead, CancellationToken.None);

            // get the file extension including the dot (e.g. ".jpg")
            string fileExtension = Path.GetExtension(imageUploadModel.Image.FileName);

            // remove the dot from the file extension (e.g. "jpg")
            string fileFormat = fileExtension.Substring(1);

            var awsFile = new AwsFile()
            {
                Id = Guid.NewGuid(),
                FileName = imageName,
                Bucket = Bucket.profile,
                CreatedDate = DateTimeOffset.UtcNow,
                HaveUse = false,
                FileFormat = fileFormat,
                Token = TokenGenerator.GenerateNewRngCrypto(),
                UserId = userDto.Id
            };
            await _awsIndexDbContext.AwsFiles.AddAsync(awsFile);
            await _awsIndexDbContext.SaveChangesAsync();

            return Ok(new TokenDto(){Token = awsFile.Token});
        }


        /// <summary>
        /// Upload user news image
        /// </summary>
        /// <response code="200">Success: Uploaded file token</response>
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [HttpPost("NewsImage")]
        [AuthorizeByIdentityServer(Roles.User + "|" + Roles.Admin)]
        public async Task<IActionResult> NewsImage([FromForm] ImageUploadModel imageUploadModel)
        {
            var jwtToken = _jwtTokenRepository.GetJwtToken();
            var userDto = _jwtTokenRepository.ExtractUserDataFromToken(jwtToken);

            string imageName = await _awsFileRepository.UploadFileAsync(Bucket.newsimage, imageUploadModel.Image, S3CannedACL.PublicRead, CancellationToken.None);

            // get the file extension including the dot (e.g. ".jpg")
            string fileExtension = Path.GetExtension(imageUploadModel.Image.FileName);

            // remove the dot from the file extension (e.g. "jpg")
            string fileFormat = fileExtension.Substring(1);

            var awsFile = new AwsFile()
            {
                Id = Guid.NewGuid(),
                FileName = imageName,
                Bucket = Bucket.newsimage,
                CreatedDate = DateTimeOffset.UtcNow,
                HaveUse = false,
                FileFormat = fileFormat,
                Token = TokenGenerator.GenerateNewRngCrypto(),
                UserId = userDto.Id
            };
            await _awsIndexDbContext.AwsFiles.AddAsync(awsFile);
            await _awsIndexDbContext.SaveChangesAsync();

            return Ok(new TokenDto() { Token = awsFile.Token });
        }


        /// <summary>
        /// Upload news video
        /// </summary>
        /// <response code="200">Success: Uploaded file token</response>
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [HttpPost("NewsVideo")]
        [AuthorizeByIdentityServer(Roles.User + "|" + Roles.Admin)]
        public async Task<IActionResult> NewsVideo([FromForm] VideoUploadModel videoUploadModel)
        {
            var jwtToken = _jwtTokenRepository.GetJwtToken();
            var userDto = _jwtTokenRepository.ExtractUserDataFromToken(jwtToken);

            string videoName = await _awsFileRepository.UploadFileAsync(Bucket.newsvideo, videoUploadModel.Video, S3CannedACL.PublicRead, CancellationToken.None);

            // get the file extension including the dot (e.g. ".jpg")
            string fileExtension = Path.GetExtension(videoUploadModel.Video.FileName);

            // remove the dot from the file extension (e.g. "jpg")
            string fileFormat = fileExtension.Substring(1);

            var awsFile = new AwsFile()
            {
                Id = Guid.NewGuid(),
                FileName = videoName,
                Bucket = Bucket.newsvideo,
                CreatedDate = DateTimeOffset.UtcNow,
                HaveUse = false,
                FileFormat = fileFormat,
                Token = TokenGenerator.GenerateNewRngCrypto(),
                UserId = userDto.Id
            };
            await _awsIndexDbContext.AwsFiles.AddAsync(awsFile);
            await _awsIndexDbContext.SaveChangesAsync();

            return Ok(new TokenDto() { Token = awsFile.Token });
        }
    }
}
