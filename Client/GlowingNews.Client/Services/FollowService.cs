﻿using Blazored.LocalStorage;
using GlowingNews.Client.Extensions;
using GlowingNews.Client.responses;
using GlowingNews.Client.responses.Base;
using GlowingNews.Client.Services.Interfaces;
using OneOf.Types;

namespace GlowingNews.Client.Services
{
    public class FollowService : IFollowService
    {
        private HttpClient _httpClient;
        ILocalStorageService _localStorageService;

        public FollowService(IHttpClientFactory httpClientFactory, ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClientFactory.CreateClient(nameof(Enums.HttpClients.User));
        }


        public async Task<CreateResponse> Follow(Guid followingId)
        {
            await _httpClient.AddAuthHeader(_localStorageService);

            await _httpClient.SendRequestAsync($"Follow/{followingId}", HttpMethod.Post);

            return new Success<Guid>(followingId);
        }

        public async Task<DeleteResponse> UnFollow(Guid unFollowId)
        {
            await _httpClient.AddAuthHeader(_localStorageService);

            await _httpClient.SendRequestAsync($"Follow/{unFollowId}", HttpMethod.Delete);

            return new Success();
        }

        public async Task<ReadResponse<List<Following>>> GetFollowingList(Guid userId)
        {
            await _httpClient.AddAuthHeader(_localStorageService);

            var response = await _httpClient.SendRequestAsync($"Follow/{userId}/FollowingList", HttpMethod.Get);

            var followingList = await JsonConverter.ToObject<List<Following>>(response.Content);

            return new Success<List<Following>>(followingList);
        }

        public async Task<ReadResponse<List<Follower>>> GetFollowerList(Guid userId)
        {
            await _httpClient.AddAuthHeader(_localStorageService);

            var response = await _httpClient.SendRequestAsync($"Follow/{userId}/FollowerList", HttpMethod.Get);

            var followingList = await JsonConverter.ToObject<List<Follower>>(response.Content);

            return new Success<List<Follower>>(followingList);
        }
    }
}