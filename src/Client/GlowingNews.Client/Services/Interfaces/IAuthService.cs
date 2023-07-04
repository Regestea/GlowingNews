using GlowingNews.Client.Models.Auth;
using GlowingNews.Client.responses;
using GlowingNews.Client.responses.Base;
using OneOf;
using OneOf.Types;

namespace GlowingNews.Client.Services.Interfaces
{
    public interface IAuthService
    {
        // Register return id
        Task<CreateResponse<Guid>> Register(RegisterModel request);

        // Login return string token
        Task<ReadResponse<JwtToken>> Login(LoginModel request);

        Task<bool> IsUserAuthenticated();
    }
}