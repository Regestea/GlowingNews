using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NewsLike.Domain.Entities;

namespace NewsLike.Application.Features.Likes.Commands.CreateLike
{
    public class CreateLikeCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid NewsId { get; set; }
    }
}