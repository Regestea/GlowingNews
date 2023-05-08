using GlowingNews.Client.Models.User;
using GlowingNews.Client.responses;
using GlowingNews.Client.responses.Base;

namespace GlowingNews.Client.Services.Interfaces
{
    public interface IUserService
    {
        Task<ReadResponse<User>> GetUser();
        Task<ReadResponse<User>> GetUser(Guid userId);
        Task<UpdateResponse> EditUser(EditUserModel editModel);
    }
}
