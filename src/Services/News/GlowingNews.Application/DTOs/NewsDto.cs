using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowingNews.Application.DTOs
{
    public class NewsDto
    {
        public Guid Id { get; set; }

        public string Text { get; set; } = null!;

        public MediaTypeDto MediaType { get; set; }

        public string? MediaPath { get; set; }

        public DateTimeOffset? CreatedDate { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; } = null!;
    }
}
