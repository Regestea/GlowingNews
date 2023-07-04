using System.ComponentModel.DataAnnotations;

namespace GlowingNews.Client.Models.News
{
    public class AddNewsModel
    {
        [MaxLength(1000)]
        public string Text { get; set; } = null!;

        public string? MediaToken { get; set; }
    }
}
