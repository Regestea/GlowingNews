using MediatR;
using NewsLike.Application.DTOs;

namespace NewsLike.Application.Features.Likes.Queries.LikedNewsList
{
    public class LikedNewsListQuery : IRequest<List<LikeDto>?>
    {
        public Guid UserId { get; set; }
    }
}