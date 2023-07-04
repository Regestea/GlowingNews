using System.ComponentModel.DataAnnotations;

namespace GlowingNews.Client.Models.Auth
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
