using UserAccount.Application.DTOs;

namespace UserAccount.Application.Common.Interfaces
{
    public interface IFollowRepository
    {
        Task<List<FollowingDto>?> FollowingListAsync(Guid userId);

        Task<List<FollowerDto>?> FollowerListAsync(Guid userId);

        Task<bool> IsFollowedAsync(Guid userId, Guid targetId);

        Task FollowAsync(Guid followerId, Guid followingId);

        Task UnFollowAsync(Guid requesterId, Guid targetId);
    }
}