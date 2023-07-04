using FluentValidation;

namespace NewsLike.Application.Features.Likes.Commands.DeleteLike
{
    public class DeleteLikeCommandValidator : AbstractValidator<DeleteLikeCommand>
    {
        public DeleteLikeCommandValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty();
            RuleFor(x => x.NewsId).NotNull().NotEmpty();
        }
    }
}
