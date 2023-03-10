using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAccount.Domain.Entities
{
    public class Follow
    {
        public Follow()
        {
             FollowedAt = DateTimeOffset.UtcNow;
        }

        [Key] 
        public Guid Id { get; set; } 

        [ForeignKey("FollowerId")]
        public User Follower { get; set; } = null!;

        [Required]
        public Guid FollowerId { get; set; }

        [ForeignKey("FollowingId")]
        public User Following { get; set; } = null!;

        [Required]
        public Guid FollowingId { get; set; }

        public DateTimeOffset FollowedAt { get; set; } 
    }
}
