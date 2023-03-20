using GlowingNews.Application.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowingNews.Application.Common.Models
{
    public class AddNewsModel
    {
        [MaxLength(1000)]
        public string Text { get; set; } = null!;

        public string? MediaToken { get; set; }

    }
}
