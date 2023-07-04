using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlowingNews.IdentityServer.Entities
{
    public class UserRole:BaseEntity
    {
        
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public  User User { get; set; } = null!;

        [Required]
        public Guid RoleId { get; set; }

        [ForeignKey("RoleId")]
        public  Role Role { get; set; } = null!;

    }
}
