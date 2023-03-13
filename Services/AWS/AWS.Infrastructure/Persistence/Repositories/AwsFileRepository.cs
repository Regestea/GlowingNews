using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using AWS.Application.Common.Interfaces;
using AWS.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace AWS.Infrastructure.Persistence.Repositories
{
    public  class AwsFileRepository : IAwsFileRepository
    {
        private readonly IAwsClientContext _amazonS3ClientContext;

        public AwsFileRepository(IAwsClientContext amazonS3ClientContext)
        {
            _amazonS3ClientContext = amazonS3ClientContext;
        }


        public async Task<string> UploadFileAsync(Buckets bucketCategory, IFormFile file, S3CannedACL acl, CancellationToken cancellationToken)
        {
            var key = Guid.NewGuid().ToString();

            using var fileTransferUtility = new TransferUtility(_amazonS3ClientContext.S3);

            await using var fileStream = new BufferedStream(file.OpenReadStream());

            var req = new TransferUtilityUploadRequest
            {
                BucketName = bucketCategory.ToString(),
                Key = key,
                InputStream = fileStream,
                AutoCloseStream = false,
                AutoResetStreamPosition = true,
                CannedACL = acl
            };

            await fileTransferUtility.UploadAsync(req, cancellationToken);

            return $"/{bucketCategory}/{key}";
        }



        public async Task DeleteFileAsync(Buckets bucketCategory, string fileName)
        {
            var req = new DeleteObjectRequest()
            {
                BucketName = bucketCategory.ToString(),
                Key = fileName.ToString()
            };
            await _amazonS3ClientContext.S3.DeleteObjectAsync(req);
        }

    }
}
