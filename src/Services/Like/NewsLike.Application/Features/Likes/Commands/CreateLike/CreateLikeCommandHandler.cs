using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NewsLike.Application.Common.Interfaces;

namespace NewsLike.Application.Features.Likes.Commands.CreateLike
{
    public class CreateLikeCommandHandler : IRequestHandler<CreateLikeCommand, bool>
    {
        private ILikeRepository _likeRepository;

        public CreateLikeCommandHandler(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public async Task<bool> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
        {
            await _likeRepository.AddLikeAsync(request.UserId, request.NewsId);

            return true;
        }
    }
}
