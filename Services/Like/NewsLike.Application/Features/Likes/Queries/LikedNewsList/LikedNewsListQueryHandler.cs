using MediatR;
using NewsLike.Application.Common.Interfaces;
using NewsLike.Application.DTOs;

namespace NewsLike.Application.Features.Likes.Queries.LikedNewsList
{
    public class LikedNewsListQueryHandler:IRequestHandler<LikedNewsListQuery,List<LikeDto>?>
    {
        private ILikeRepository _likeRepository;

        public LikedNewsListQueryHandler(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }


        public async Task<List<LikeDto>?> Handle(LikedNewsListQuery request, CancellationToken cancellationToken)
        {
            var likeList =await _likeRepository.LikedNewsListAsync(request.UserId);

            return likeList;
        }
    }
}
