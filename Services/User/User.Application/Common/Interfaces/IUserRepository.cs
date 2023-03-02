
using UserAccount.Application.Common.Models;

namespace UserAccount.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<UserModel?> GetUserAsync(Guid userId);

        Task<Guid> CreateUserAsync(CreateUserModel userModel);

    }
}
