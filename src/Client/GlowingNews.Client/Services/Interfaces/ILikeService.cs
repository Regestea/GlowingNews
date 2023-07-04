using GlowingNews.Client.responses;
using GlowingNews.Client.responses.Base;

namespace GlowingNews.Client.Services.Interfaces
{
    public interface ILikeService
    {
        Task<ReadResponse<bool>> IsLikedAsync(Guid newsId);
        Task<ReadResponse<int>> NewsLikeCountAsync(Guid newsId);

        Task<ReadResponse<List<Like>?>> LikedNewsListAsync();

        Task AddLikeAsync(Guid newsId);

        Task DeleteLikeAsync(Guid newsId);
    }
}
