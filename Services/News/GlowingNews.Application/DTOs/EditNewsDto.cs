using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowingNews.Application.DTOs
{
    public class EditNewsDto
    {
        public string Text { get; set; } = null!;

        public Guid UserId { get; set; }

        public Guid NewsId { get; set; }

    }
}
