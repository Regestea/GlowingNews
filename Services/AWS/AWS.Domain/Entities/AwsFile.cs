using AWS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWS.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace AWS.Domain.Entities
{
    public class AwsFile: BaseEntity
    {
        [Required]
        public Bucket Bucket{ get; set; }

        [Required]
        public string FileName { get; set; } = null!;

        [Required]
        public bool HaveUse { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Token { get; set; } = null!;

        [NotMapped]
        public string FullPath => $"{Bucket}"+"/"+$"{FileName}";
    }
}
