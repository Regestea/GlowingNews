using AWS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Application.Models
{
    public class DeleteFilesModel
    {
        public Bucket Bucket { get; set; }
        public List<string> FilesName { get; set; } = null!;
    }
}
