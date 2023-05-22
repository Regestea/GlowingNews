using GlowingNews.Client.DTOs.Response;
using GlowingNews.Client.Models.File;
using GlowingNews.Client.responses;
using GlowingNews.Client.responses.Base;
using Microsoft.AspNetCore.Components.Forms;

namespace GlowingNews.Client.Services.Interfaces
{
    public interface IUploadService
    {
        Task<CreateResponse<JwtToken>> UserProfileImage(ImageUploadModel request,Action<UploadPercentageDto> onProgress);
    }
}
