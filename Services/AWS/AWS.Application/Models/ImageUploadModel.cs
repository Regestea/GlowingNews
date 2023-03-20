using AWS.Application.Common.Filters;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AWS.Application.Models
{
    public class ImageUploadModel
    {
        [FileSizeMegabyte(0.1, 15)]
        [FileAllowedExtensions(".jpg", ".jpeg", ".png",".gif")]
        [Required]
        public IFormFile Image { get; set; } = null!;
    }
}
