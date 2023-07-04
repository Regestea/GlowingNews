using MediatR;

namespace NewsLike.Application.Features.Likes.Queries.NewsLikeCount
{
    public class NewsLikeCountQuery : IRequest<int>
    {
        public Guid NewsId { get; set; }
    }
}