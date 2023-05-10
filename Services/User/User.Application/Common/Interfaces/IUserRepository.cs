﻿
using UserAccount.Application.Common.Models;
using UserAccount.Application.DTOs;

namespace UserAccount.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDto?> GetUserAsync(Guid userId);
        Task<List<UserDto>?> SearchUserAsync(string userName);

        Task<Guid> CreateUserAsync(CreateUserDto userModel);

        Task EditUserAsync(Guid userId,EditUserModel user);

    }
}
