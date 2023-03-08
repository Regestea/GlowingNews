using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlowingNews.Domain.Common;
using GlowingNews.Domain.Enums;

namespace GlowingNews.Domain.Entities
{
    public class New:BaseEntity
    {
        [Required]
        [MaxLength(1000)]
        public string Text { get; set; } = null!;

        [Required]
        public MediaType MediaType { get; set; }

        public string? MediaPath { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = null!;
    }
}
