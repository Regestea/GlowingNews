using Amazon.S3;
using AWS.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace AWS.Application.Common.Interfaces
{
    public interface IAwsFileRepository
    {
        Task<string> UploadFileAsync(Buckets bucketCategory, IFormFile file, S3CannedACL ac, CancellationToken cancellationTokenl);
        Task DeleteFileAsync(Buckets bucketCategory, string fileName);
    }
}
