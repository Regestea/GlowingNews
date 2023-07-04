using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using AWS.Application.Common.Interfaces;
using AWS.Application.Models;
using AWS.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace AWS.Infrastructure.Persistence.Repositories
{
    public class AwsFileRepository : IAwsFileRepository
    {
        private readonly IAwsClientContext _amazonS3ClientContext;

        public AwsFileRepository(IAwsClientContext amazonS3ClientContext)
        {
            _amazonS3ClientContext = amazonS3ClientContext;
        }


        public async Task<string> UploadFileAsync(Bucket bucket, IFormFile file, S3CannedACL fileAccess, CancellationToken cancellationToken)
        {
            var key = Guid.NewGuid().ToString();

            using var fileTransferUtility = new TransferUtility(_amazonS3ClientContext.S3);

            await using var fileStream = new BufferedStream(file.OpenReadStream());

            var req = new TransferUtilityUploadRequest
            {
                BucketName = bucket.ToString(),
                Key = key,
                InputStream = fileStream,
                AutoCloseStream = false,
                AutoResetStreamPosition = true,
                CannedACL = fileAccess
            };

            await fileTransferUtility.UploadAsync(req, cancellationToken);

            return $"{key}";
        }


        public async Task DeleteFilesAsync(DeleteFilesModel model)
        {
            var awsFilesNameList = new List<KeyVersion>();
            foreach (var file in model.FilesName)
            {
                awsFilesNameList.Add(new KeyVersion() { Key = file });
            }
            var requests = new DeleteObjectsRequest() { BucketName = model.Bucket.ToString(), Objects = awsFilesNameList };
            await _amazonS3ClientContext.S3.DeleteObjectsAsync(requests);
            
        }
    }
}
