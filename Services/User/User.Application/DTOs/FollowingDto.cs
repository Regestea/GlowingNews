using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAccount.Application.DTOs
{
    public class FollowingDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string? UserImage { get; set; } 

    }
}
