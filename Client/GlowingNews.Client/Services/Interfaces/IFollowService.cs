﻿using GlowingNews.Client.responses;
using GlowingNews.Client.responses.Base;

namespace GlowingNews.Client.Services.Interfaces
{
    public interface IFollowService
    {
        Task<CreateResponse> Follow(Guid followingId);
        Task<DeleteResponse> UnFollow(Guid unFollowId);
        Task<ReadResponse<List<Following>>> GetFollowingList(Guid userId);
        Task<ReadResponse<List<Follower>>> GetFollowerList(Guid userId);
    }
}