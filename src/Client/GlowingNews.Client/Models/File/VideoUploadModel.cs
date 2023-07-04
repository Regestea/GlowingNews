using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace GlowingNews.Client.Models.File
{
    public class VideoUploadModel
    {
        [Required]
        public IBrowserFile Video { get; set; }
    }
}
