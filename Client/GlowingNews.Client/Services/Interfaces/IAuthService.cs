using GlowingNews.Client.Models.Auth;
using GlowingNews.Client.responses.Base;
using OneOf;
using OneOf.Types;

namespace GlowingNews.Client.Services.Interfaces
{
    public interface IAuthService
    {
        // Register return id
        Task<CreateResponse> Register(RegisterModel request);

        // Login return string token
        Task<ReadResponse<string>> Login(LoginModel request);

        Task<bool> IsUserAuthenticated();
    }
}