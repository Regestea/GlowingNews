using Amazon.S3;
using AWS.Application.Models;
using AWS.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace AWS.Application.Common.Interfaces
{
    public interface IAwsFileRepository
    {
        Task<string> UploadFileAsync(Bucket bucket, IFormFile file, S3CannedACL fileAccess, CancellationToken cancellationTokenl);
        Task DeleteFilesAsync(DeleteFilesModel model);
    }
}
