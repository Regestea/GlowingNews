using GlowingNews.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using UserAccount.Application.Common.Interfaces;
using UserAccount.Application.Common.Models;
using UserAccount.Application.DTOs;
using UserAccount.Domain.Entities;
using UserAccount.Infrastructure.Persistence;

namespace GlowingNews.Infrastructure.Repositories
{
    public class UserRepository: IUserRepository
    {
        private UserAccountContext _context;

        public UserRepository(UserAccountContext context)
        {
            _context = context;
        }

        public async Task<UserDto?> GetUserAsync(Guid userId)
        {
            var user=await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if (user == null) return null;

            var userModel = new UserDto()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                About = user.About,
                Image = user.Image
            };

            return userModel;
        }

        public async Task<Guid> CreateUserAsync(CreateUserDto userModel)
        {
            var user = new User()
            {
                Id = userModel.Id,
                Name = userModel.Name!,
                Email = userModel.Email!
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task EditUserAsync(Guid userId,EditUserModel userModel)
        {
            var user=await _context.Users.SingleAsync(x => x.Id == userId);
            user.About=userModel.About;
            user.ModifiedDate=DateTimeOffset.Now;
            _context.Users.Update(user);
        }
    }
}
