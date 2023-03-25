namespace NewsLike.Application.DTOs
{
    public class LikeDto
    {
        public Guid Id { get; set; }

        public Guid NewsId { get; set; }

        public DateTimeOffset? CreatedDate { get; set; }
    }
}
