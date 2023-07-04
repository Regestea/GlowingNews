using Microsoft.EntityFrameworkCore;
using NewsLike.Application.Common.Interfaces;
using NewsLike.Application.DTOs;
using NewsLike.Domain.Entities;
using NewsLike.Infrastructure.Persistence;

namespace NewsLike.Infrastructure.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly LikeContext _likeContext;

        public LikeRepository(LikeContext likeContext)
        {
            _likeContext = likeContext;
        }

        public async Task<bool> IsLikedAsync(Guid userId, Guid newsId)
        {
            return await _likeContext.Likes.AnyAsync(x => x.UserId == userId && x.NewsId == newsId);
        }

        public async Task<int> NewsLikeCountAsync(Guid newsId)
        {
            return await _likeContext.Likes.Where(x => x.NewsId == newsId).CountAsync();
        }

        public async Task<List<LikeDto>?> LikedNewsListAsync(Guid userId)
        {
            var likeDto =await _likeContext.Likes.Where(x => x.UserId == userId).Select(x => new LikeDto()
            {
                Id = x.Id,
                NewsId = x.NewsId,
                CreatedDate = x.CreatedDate
            }).ToListAsync();

            return likeDto;
        }

        public async Task AddLikeAsync(Guid userId, Guid newsId)
        {
            bool alreadyLiked =await _likeContext.Likes.AnyAsync(x => x.UserId == userId && x.NewsId == newsId);

            if (!alreadyLiked)
            {
                var like = new Like()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    NewsId = newsId,
                    CreatedDate = DateTimeOffset.UtcNow
                };
                await _likeContext.Likes.AddAsync(like);

                await _likeContext.SaveChangesAsync();
            }
        }

        public async Task DeleteLikeAsync(Guid userId, Guid newsId)
        {
            var like =await _likeContext.Likes.SingleOrDefaultAsync(x => x.UserId == userId && x.NewsId == newsId);

            if (like !=null)
            {
                _likeContext.Likes.Remove(like);
                await _likeContext.SaveChangesAsync();
            }
        }
    }
}
