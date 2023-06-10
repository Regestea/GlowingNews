using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowingNews.Application.DTOs
{
    public class NewsDailyDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
    }
}
