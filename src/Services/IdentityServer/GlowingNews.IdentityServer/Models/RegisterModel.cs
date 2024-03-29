﻿using System.ComponentModel.DataAnnotations;

namespace GlowingNews.IdentityServer.Models
{
    public class RegisterModel
    {
        [Required]
        [MaxLength(50)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Password { get; set; } = null!;
    }
}
