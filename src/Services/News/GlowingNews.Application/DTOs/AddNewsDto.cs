using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowingNews.Application.DTOs
{
    public class AddNewsDto
    {
        public string Text { get; set; } = null!;

        public MediaTypeDto MediaType { get; set; }

        public string? MediaPath { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; } = null!;
    }
}
