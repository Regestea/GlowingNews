using System.ComponentModel.DataAnnotations;
using UserAccount.Domain.Common;

namespace UserAccount.Domain.Entities
{
    public class User : BaseEntity
    {

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Email { get; set; } = null!;

        
        [MaxLength(100)]
        public string? About { get; set; }


        public string? Image { get; set; }

    }
}
