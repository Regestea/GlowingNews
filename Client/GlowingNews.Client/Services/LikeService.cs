using Blazored.LocalStorage;
using GlowingNews.Client.Extensions;
using GlowingNews.Client.responses;
using GlowingNews.Client.responses.Base;
using GlowingNews.Client.Services.Interfaces;
using OneOf.Types;

namespace GlowingNews.Client.Services
{
    public class LikeService: ILikeService
    {
        private HttpClient _httpClient;
        ILocalStorageService _localStorageService;

        public LikeService(IHttpClientFactory httpClientFactory, ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClientFactory.CreateClient(nameof(Enums.HttpClients.Like));
        }

        public async Task<ReadResponse<bool>> IsLikedAsync(Guid newsId)
        {
            await _httpClient.AddAuthHeader(_localStorageService);

            var response=await _httpClient.SendRequestAsync($"Like/{newsId}", HttpMethod.Get);

            bool isLiked = await JsonConverter.ToObject<bool>(response.Content);

            return new Success<bool>(isLiked);
        }

        public async Task<ReadResponse<int>> NewsLikeCountAsync(Guid newsId)
        {
            await _httpClient.AddAuthHeader(_localStorageService);

            var response = await _httpClient.SendRequestAsync($"Like/{newsId}/Count", HttpMethod.Get);

            int likeCount = await JsonConverter.ToObject<int>(response.Content);

            return new Success<int>(likeCount);
        }

        public async Task<ReadResponse<List<Like>?>> LikedNewsListAsync()
        {
            await _httpClient.AddAuthHeader(_localStorageService);

            var response = await _httpClient.SendRequestAsync($"Like/List", HttpMethod.Get);

           var likeList= await JsonConverter.ToObject<List<Like>?>(response.Content);

            return new Success<List<Like>?>(likeList);
        }

        public async Task AddLikeAsync(Guid newsId)
        {
            await _httpClient.AddAuthHeader(_localStorageService);
            
            await _httpClient.SendRequestAsync($"Like/{newsId}", HttpMethod.Post);
        }

        public async Task DeleteLikeAsync(Guid newsId)
        {
            await _httpClient.AddAuthHeader(_localStorageService);

            await _httpClient.SendRequestAsync($"Like/{newsId}", HttpMethod.Delete);
        }
    }
}
