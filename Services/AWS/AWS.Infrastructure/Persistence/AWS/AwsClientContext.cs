using Amazon.S3;
using Amazon.S3.Model;
using AWS.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AWS.Infrastructure.Persistence.AWS
{
    public class AwsClientContext : IAwsClientContext
    {

        public AwsClientContext(IConfiguration configuration)
        {
            var awsConfig = configuration.GetSection(nameof(AwsSettings));

            var awsSettings = awsConfig.Get<AwsSettings>();
            var s3Config = new AmazonS3Config
            {
                ServiceURL = awsSettings?.ServiceUrl,
                MaxErrorRetry = 3,
                ForcePathStyle = true,
                SignatureVersion = "s3v4"
            };

            S3 = new AmazonS3Client(awsSettings?.AccessKey, awsSettings?.SecretKey, s3Config);

            if (awsSettings?.Buckets != null) S3.SeedingData(awsSettings.Buckets).Wait();
        }


        public AmazonS3Client S3 { get; set; }
    }
}
