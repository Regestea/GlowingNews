using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAccount.Application.DTOs
{
    public class FollowDto
    {
        public Guid Id { get; set; }

        public Guid FollowerId { get; set; }

        public string FollowerName { get; set; } = null!;

        public string FollowerImage { get; set; } = null!;

        public Guid FollowingId { get; set; }

        public string FollowingName { get; set; } = null!;

        public string FollowingImage { get; set; } = null!;

        public DateTimeOffset? FollowedAt { get; set; }
    }
}
