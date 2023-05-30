using System.ComponentModel.DataAnnotations;

namespace GlowingNews.Client.Models.News
{
    public class EditNewsModel
    {
        [MaxLength(1000)]
        public string Text { get; set; } = null!;
    }
}
