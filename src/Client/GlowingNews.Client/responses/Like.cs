namespace GlowingNews.Client.responses
{
    public class Like
    {
        public Guid Id { get; set; }

        public Guid NewsId { get; set; }

        public DateTimeOffset? CreatedDate { get; set; }
    }
}
