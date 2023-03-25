using MediatR;
using NewsLike.Application.Common.Interfaces;

namespace NewsLike.Application.Features.Likes.Queries.IsLiked
{
    public class NewsLikeCountQueryHandler : IRequestHandler<IsLikedQuery, bool>
    {
        private ILikeRepository _likeRepository;

        public NewsLikeCountQueryHandler(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public async Task<bool> Handle(IsLikedQuery request, CancellationToken cancellationToken)
        {
            return await _likeRepository.IsLikedAsync(request.UserId, request.NewsId);
        }
    }
}
