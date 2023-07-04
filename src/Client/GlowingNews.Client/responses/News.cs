namespace GlowingNews.Client.responses
{
    public class News
    {
        public Guid Id { get; set; }

        public string Text { get; set; } = null!;

        public MediaType MediaType { get; set; }

        public string? MediaPath { get; set; }

        public DateTimeOffset? CreatedDate { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; } = null!;
    }
}
