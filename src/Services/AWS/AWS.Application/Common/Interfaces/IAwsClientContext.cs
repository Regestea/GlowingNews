using Amazon.S3;

namespace AWS.Application.Common.Interfaces
{
    public interface IAwsClientContext
    {
        public AmazonS3Client S3 { get; set; }
    }
}
