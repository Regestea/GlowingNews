using AWS.Application.Common.Filters;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AWS.Application.Models
{
    public class ImageUploadModel
    {
        [FileSizeMegabyte(1, 5 * 1024)]
        [FileAllowedExtensions(".jpg", ".jpeg", ".png")]
        [Required]
        public IFormFile Image { get; set; } = null!;
    }
}
