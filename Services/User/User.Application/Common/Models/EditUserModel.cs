using System.ComponentModel.DataAnnotations;

namespace UserAccount.Application.Common.Models
{
    public class EditUserModel
    {
        [MaxLength(100)]
        public string? About { get; set; }
        
    }
}
