using System.ComponentModel.DataAnnotations;

namespace GlowingNews.IdentityServer.Entities
{
    public class Role:BaseEntity
    {
        
        [Required]
        public string RoleTitle { get; set; } = null!;

    }
}
