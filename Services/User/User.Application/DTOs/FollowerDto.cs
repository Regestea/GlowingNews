using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAccount.Application.DTOs
{
    public class FollowerDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string? UserImage { get; set; }

        public DateTimeOffset? FollowedAt { get; set; }
    }
}
