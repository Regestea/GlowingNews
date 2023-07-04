using Blazored.LocalStorage;
using GlowingNews.Client.Extensions;
using GlowingNews.Client.responses;
using GlowingNews.Client.responses.Base;
using GlowingNews.Client.Services.Interfaces;
using OneOf.Types;
using System.Collections.Generic;
using System.Net;

namespace GlowingNews.Client.Services
{
    public class SearchService: ISearchService
    {
        private HttpClient _httpClient;
        ILocalStorageService _localStorageService;

        public SearchService(IHttpClientFactory httpClientFactory, ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClientFactory.CreateClient(nameof(Enums.HttpClients.Search));
        }

        public async Task<ReadResponse<List<NewsSearch>>> SearchNews(string keyWord)
        {
            //await _httpClient.AddAuthHeader(_localStorageService);
           
            var response = await _httpClient.SendRequestAsync($"NewsSearch?keyword={keyWord}", HttpMethod.Get);
           
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return await JsonConverter.ToValidationFailedList(response.Content);
            }

            var searchResult = await JsonConverter.ToObject<List<NewsSearch>>(response.Content);

            return new Success<List<NewsSearch>> (searchResult);
        }
    }
}
