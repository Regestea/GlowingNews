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


        public async Task<List<FollowingDto>?> FollowingListAsync(Guid userId)
        {
            var followerList = await _context.Follows
                .Where(x => x.FollowerId == userId)
                .Include(x => x.Following)
                .Select(x => new FollowingDto()
                {
                    Id = x.Id,
                    UserId = x.FollowingId,
                    UserName = x.Following.Name,
                    UserImage = x.Following.Image
                })
                .ToListAsync();

            return followerList;
        }

        public async Task<List<FollowerDto>?> FollowerListAsync(Guid userId)
        {
            var followingList = await _context.Follows
                .Where(x => x.FollowingId == userId)
                .Include(x => x.Follower)
                .Select(x => new FollowerDto()
                {
                    Id = x.Id,
                    UserId = x.FollowerId,
                    UserName = x.Follower.Name,
                    UserImage = x.Follower.Image
                })
                .AsSplitQuery()
                .ToListAsync();
            return followingList;
        }

        public async Task<bool> IsFollowedAsync(Guid userId, Guid targetId)
        {
            return await _context.Follows.AnyAsync(x => x.FollowerId == userId && x.FollowingId == targetId);
        }

        public async Task FollowAsync(Guid followerId, Guid followingId)
        {
            var existFollow = await _context.Follows.AnyAsync(x => x.FollowerId == followerId && x.FollowingId == followingId);
            if (existFollow) return;

            var existFollower = await _context.Users.AnyAsync(x => x.Id == followerId);
            var existFollowing = await _context.Users.AnyAsync(x => x.Id == followingId);


            if (existFollower && existFollowing)
            {
                await _context.Follows.AddAsync(new Follow() { Id = Guid.NewGuid(), FollowerId = followerId, FollowingId = followingId });
                await _context.SaveChangesAsync();
            }
        }

        public async Task UnFollowAsync(Guid requesterId, Guid targetId)
        {
            var follow =
                await _context.Follows.FirstOrDefaultAsync(
                    x => x.FollowerId == requesterId && x.FollowingId == targetId);

            if (follow != null)
            {
                _context.Follows.Remove(follow);
                await _context.SaveChangesAsync();
            }
        }
    }
}