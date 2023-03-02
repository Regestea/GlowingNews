using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserAccount.Application.Common.Interfaces;
using UserAccount.Application.Common.Models;
using UserAccount.Domain.Entities;
using UserAccount.Infrastructure.Persistence;

namespace UserAccount.Infrastructure.Repositories
{
    public class UserRepository: IUserRepository
    {
        private UserAccountContext _context;

        public UserRepository(UserAccountContext context)
        {
            _context = context;
        }

        public async Task<UserModel?> GetUserAsync(Guid userId)
        {
            var user=await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if (user == null) return null;

            var userModel = new UserModel()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                About = user.About,
                Image = user.Image
            };

            return userModel;
        }

        public async Task<Guid> CreateUserAsync(CreateUserModel userModel)
        {
            var user = new User()
            {
                Id = userModel.Id,
                Name = userModel.Name!,
                Email = userModel.Email!,
                About = userModel.About,
                Image = userModel.Image
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }
    }
}
