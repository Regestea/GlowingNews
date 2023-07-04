namespace GlowingNews.Client.responses
{
    public class Following
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string? UserImage { get; set; }

    }
}
