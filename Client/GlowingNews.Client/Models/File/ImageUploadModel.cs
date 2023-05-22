using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace GlowingNews.Client.Models.File
{
    public class ImageUploadModel
    {
        [Required]
        public IBrowserFile Image { get; set; }
    }
}
