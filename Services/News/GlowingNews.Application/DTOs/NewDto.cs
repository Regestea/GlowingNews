using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowingNews.Application.DTOs
{
    public class NewDto
    {
        public Guid Id { get; set; }

        public string Text { get; set; } = null!;

        public MediaTypeDto MediaType { get; set; }

        public string? MediaPath { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; } = null!;
    }
}
