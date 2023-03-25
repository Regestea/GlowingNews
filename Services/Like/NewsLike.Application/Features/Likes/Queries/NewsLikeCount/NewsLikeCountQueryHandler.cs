using MediatR;
using NewsLike.Application.Common.Interfaces;

namespace NewsLike.Application.Features.Likes.Queries.NewsLikeCount
{
    public class NewsLikeCountQueryHandler : IRequestHandler<NewsLikeCountQuery, int>
    {
        private ILikeRepository _likeRepository;

        public NewsLikeCountQueryHandler(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }


        public async Task<int> Handle(NewsLikeCountQuery request, CancellationToken cancellationToken)
        {
            return await _likeRepository.NewsLikeCountAsync(request.NewsId);
        }
    }
}
