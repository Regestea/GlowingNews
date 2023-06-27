using Blazored.LocalStorage;
using GlowingNews.Client.Extensions;
using GlowingNews.Client.Models.News;
using GlowingNews.Client.responses;
using GlowingNews.Client.responses.Base;
using GlowingNews.Client.Services.Interfaces;
using OneOf.Types;
using System.Net;

namespace GlowingNews.Client.Services
{
    public class NewsService : INewsService
    {

        private HttpClient _httpClient;
        ILocalStorageService _localStorageService;

        public NewsService(IHttpClientFactory httpClientFactory, ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClientFactory.CreateClient(nameof(Enums.HttpClients.News));
        }


        public async Task<CreateResponse<IdResult>> AddNews(AddNewsModel newsModel)
        {
            var requestStringContent = await JsonConverter.ToStringContent(newsModel);

            await _httpClient.AddAuthHeader(_localStorageService);

            var response = await _httpClient.SendRequestAsync("News", HttpMethod.Post, requestStringContent);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return await JsonConverter.ToValidationFailedList(response.Content);
            }

            var idResult = await JsonConverter.ToObject<IdResult>(response.Content);

            return new Success<IdResult>(idResult);
        }

        public async Task<UpdateResponse> EditNews(Guid newsId,EditNewsModel newsModel)
        {
            var requestStringContent = await JsonConverter.ToStringContent(newsModel);

            await _httpClient.AddAuthHeader(_localStorageService);

            var response = await _httpClient.SendRequestAsync($"News/{newsId}", HttpMethod.Patch, requestStringContent);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return new Error<string>("something went wrong");
            }

            return new Success();
        }

        public async Task<DeleteResponse> DeleteNews(Guid newsId)
        {
            await _httpClient.AddAuthHeader(_localStorageService);

            await _httpClient.SendRequestAsync($"News/{newsId}", HttpMethod.Delete);

            return new Success();
        }

        public async Task<ReadResponse<News>> GetNews(Guid id)
        {
            await _httpClient.AddAuthHeader(_localStorageService);

            var response = await _httpClient.SendRequestAsync($"News/{id}", HttpMethod.Get);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var news = await JsonConverter.ToObject<News>(response.Content);

                return new Success<News>(news);
            }

            return new NotFound();
        }

        public async Task<ReadResponse<List<News>>> GetNewsList(Guid userId)
        {
            await _httpClient.AddAuthHeader(_localStorageService);

            var response = await _httpClient.SendRequestAsync($"News/{userId}/List", HttpMethod.Get);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var news = await JsonConverter.ToObject<List<News>>(response.Content);

                return new Success<List<News>>(news);
            }

            return new NotFound();
        }

        public async Task<ReadResponse<List<NewsDaily>>> GetDailyNewsList(List<Guid> followingIdList)
        {
            await _httpClient.AddAuthHeader(_localStorageService);

            var requestStringContent = await JsonConverter.ToStringContent(followingIdList);

            var response = await _httpClient.SendRequestAsync("News/Daily/List", HttpMethod.Get, requestStringContent);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var news = await JsonConverter.ToObject<List<NewsDaily>>(response.Content);

                return new Success<List<NewsDaily>>(news);
            }

            return new Success<List<NewsDaily>>(new List<NewsDaily>());
        }
    }
}
