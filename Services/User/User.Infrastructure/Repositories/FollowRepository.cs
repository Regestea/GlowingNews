using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserAccount.Application.Common.Interfaces;
using UserAccount.Application.DTOs;
using UserAccount.Domain.Entities;
using UserAccount.Infrastructure.Persistence;

namespace UserAccount.Infrastructure.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        private UserAccountContext _context;

        public FollowRepository(UserAccountContext context)
        {
            _context = context;
        }


        public async Task<List<FollowingDto>> FollowingList(Guid userId)
        {
            var followerList =await _context.Follows
                .Where(x => x.FollowingId == userId)
                .Include(x => x.Follower)
                .Select(x => new FollowingDto()
                {
                    Id = x.Id,
                    UserId = x.FollowerId,
                    UserName = x.Follower.Name,
                    UserImage = x.Follower.Image,
                    FollowedAt = x.FollowedAt
                })
                .ToListAsync();
            return followerList;
        }

        public async Task<List<FollowerDto>> FollowerList(Guid userId)
        {
            var followingList =await _context.Follows
                .Where(x => x.FollowerId == userId)
                .Include(x => x.Following)
                .Select(x => new FollowerDto()
                {
                    Id = x.Id,
                    UserId =x.FollowingId,
                    UserName = x.Following.Name,
                    UserImage = x.Following.Image
                }).ToListAsync();
            return followingList;
        }

        public async Task Follow(Guid followerId, Guid followingId)
        {
            var existFollowerTask = _context.Users.AnyAsync(x => x.Id == followerId);
            var existFollowingTask = _context.Users.AnyAsync(x => x.Id == followerId);

            var exist = await Task.WhenAll(existFollowingTask, existFollowerTask);
            if (exist[0] && exist[1])
            {
                await _context.Follows.AddAsync(new Follow() { FollowerId = followerId, FollowingId = followingId });
            }
        }

        public async Task UnFollow(Guid requesterId, Guid targetId)
        {
            var follow =
                await _context.Follows.FirstOrDefaultAsync(
                    x => x.FollowerId == requesterId && x.FollowingId == targetId);

            if (follow != null)
            {
                _context.Follows.Remove(follow);
            }
        }
    }
}