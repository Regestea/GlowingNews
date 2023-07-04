namespace AWS.Infrastructure.Persistence.AWS
{
    public interface IAwsSettings
    {
        public string ServiceUrl { get; set; }
        public string AccessKey { get; set; }
        string SecretKey { get; set; }
        Dictionary<string, string> Buckets { get; set; }

    }

    public class AwsSettings : IAwsSettings
    {
        public string ServiceUrl { get; set; } = null!;
        public string AccessKey { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
        public Dictionary<string, string> Buckets { get; set; } = null!;
    }
}