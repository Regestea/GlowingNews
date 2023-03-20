using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWS.Shared.Client.GrpcServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserAccount.Application.Common.Interfaces;
using UserAccount.Application.Common.Models;
using UserAccount.Application.DTOs;
using UserAccount.Domain.Entities;
using UserAccount.Infrastructure.Persistence;

namespace UserAccount.Infrastructure.Repositories
{
    public class UserRepository: IUserRepository
    {
        private UserAccountContext _context;
        private AwsGrpcService _awsGrpcService;

        public UserRepository(UserAccountContext context, AwsGrpcService awsGrpcService)
        {
            _context = context;
            _awsGrpcService = awsGrpcService;
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
            if (userModel.ProfileImageToken !=null)
            {
                var imagePath =await _awsGrpcService.GetObjectPathAsync(userId, userModel.ProfileImageToken);

                if (!imagePath.FilePath.IsNullOrEmpty())
                {
                    if (user.Image != null)
                    {
                        await _awsGrpcService.DeleteObjectAsync(userId, user.Image);
                    }

                    user.Image = imagePath.FilePath;
                }
            }

            if (userModel.About !=null)
            {
                user.About = userModel.About;
            }
            
            user.ModifiedDate=DateTimeOffset.UtcNow;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
