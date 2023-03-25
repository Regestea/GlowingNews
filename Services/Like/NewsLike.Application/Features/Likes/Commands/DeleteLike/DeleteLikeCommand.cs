using MediatR;

namespace NewsLike.Application.Features.Likes.Commands.DeleteLike
{
    public class DeleteLikeCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid NewsId { get; set; }
    }
}