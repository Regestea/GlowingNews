using MediatR;
using NewsLike.Application.Common.Interfaces;

namespace NewsLike.Application.Features.Likes.Commands.DeleteLike
{
    public class DeleteLikeCommandHandler : IRequestHandler<DeleteLikeCommand>
    {
        private ILikeRepository _likeRepository;

        public DeleteLikeCommandHandler(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public async Task<Unit> Handle(DeleteLikeCommand request, CancellationToken cancellationToken)
        {
            await _likeRepository.DeleteLikeAsync(request.UserId, request.NewsId);

            return Unit.Value;
        }
    }
}
