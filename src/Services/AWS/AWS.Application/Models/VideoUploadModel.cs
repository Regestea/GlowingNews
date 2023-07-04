using AWS.Application.Common.Filters;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS.Application.Models
{
    public class VideoUploadModel
    {
        [FileSizeMegabyte(0.1, 100)]
        [FileAllowedExtensions(".mp4", ".mkv")]
        [Required]
        public IFormFile Video { get; set; } = null!;
    }
}
