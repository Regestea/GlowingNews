using UserAccount.Application.DTOs;

namespace UserAccount.Application.Common.Interfaces
{
    public interface IFollowRepository
    {
        Task<List<FollowingDto>> FollowingList(Guid userId);

        Task<List<FollowerDto>> FollowerList(Guid userId);

        Task Follow(Guid followerId, Guid followingId);

        Task UnFollow(Guid followerId, Guid followingId);
    }
}