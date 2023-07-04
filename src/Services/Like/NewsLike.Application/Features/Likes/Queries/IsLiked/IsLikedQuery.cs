using MediatR;

namespace NewsLike.Application.Features.Likes.Queries.IsLiked
{
    public class IsLikedQuery : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid NewsId { get; set; }
    }
}