using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace NewsLike.Application.Features.Likes.Commands.CreateLike
{
    public class CreateLikeCommandValidator : AbstractValidator<CreateLikeCommand>
    {
        public CreateLikeCommandValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty();
            RuleFor(x => x.NewsId).NotNull().NotEmpty();
        }
    }
}
