using NewsLike.Application.DTOs;

namespace NewsLike.Application.Common.Interfaces
{
    public interface ILikeRepository
    {
        Task<bool> IsLikedAsync(Guid userId, Guid newsId);
        Task<int> NewsLikeCountAsync(Guid newsId);

        Task<List<LikeDto>?> LikedNewsListAsync(Guid userId);

        Task<Guid> AddLikeAsync(Guid userId, Guid newsId);

        Task DeleteLikeAsync(Guid userId, Guid newsId);
    }
}
