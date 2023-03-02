using System.ComponentModel.DataAnnotations;

namespace UserAccount.Application.Common.Models
{
    public class CreateUserModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required] 
        [MaxLength(50)] 
        public string? Name { get; set; } 

        [Required]
        [MaxLength(50)]
        public string? Email { get; set; } 


        [MaxLength(100)]
        public string? About { get; set; }


        public string? Image { get; set; }
    }
}
