using System.ComponentModel.DataAnnotations;

namespace GlowingNews.Client.Models.User
{
    public class EditUserModel
    {
        [MaxLength(100)]
        public string? About { get; set; }

        [MaxLength(100)]
        public string? ProfileImageToken { get; set; }
    }
}
