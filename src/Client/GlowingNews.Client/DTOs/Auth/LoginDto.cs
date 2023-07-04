using System.ComponentModel.DataAnnotations;

namespace GlowingNews.Client.DTOs.Auth
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
