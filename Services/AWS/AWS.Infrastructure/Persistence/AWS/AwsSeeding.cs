using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace AWS.Infrastructure.Persistence.AWS
{
    public static class AwsSeeding
    {
        public static async Task SeedingData(this AmazonS3Client _S3client, Dictionary<string, string> shouldBuckets)
        {
            var currentBuckets = await _S3client.ListBucketsAsync();

            foreach (var shouldBucket in shouldBuckets)
            {
                var existBucket = currentBuckets.Buckets.Any(x => x.BucketName == shouldBucket.Key);
                if (existBucket)
                {
                    continue;
                }
                else
                {
                    _S3client.PutBucketAsync(new PutBucketRequest()
                    {
                        BucketName = shouldBucket.Key,
                        CannedACL = S3CannedACL.PublicRead
                    }).Wait();
                }
            }
        }
    }
}
