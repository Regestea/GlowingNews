using System.ComponentModel.DataAnnotations;

namespace GlowingNews.IdentityServer.Entities
{
    public class User:BaseEntity
    {
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Email { get; set; } = null!;

        #region Relations

        public List<UserRole>? UserRoles { get; set; }

        #endregion
    }
}
