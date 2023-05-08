using Blazored.LocalStorage;
using GlowingNews.Client.Extensions;
using GlowingNews.Client.Models.User;
using GlowingNews.Client.responses;
using GlowingNews.Client.responses.Base;
using GlowingNews.Client.Services.Interfaces;
using OneOf.Types;
using System.Net;

namespace GlowingNews.Client.Services
{
    public class UserService : IUserService
    {
        private HttpClient _httpClient;
        ILocalStorageService _localStorageService;

        public UserService(IHttpClientFactory httpClientFactory, ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClientFactory.CreateClient(nameof(Enums.HttpClients.User));
        }

        public async Task<ReadResponse<User>> GetUser()
        {
            await _httpClient.AddAuthHeader(_localStorageService);

            var response = await _httpClient.SendRequestAsync("User", HttpMethod.Get);

            var user = await JsonConverter.ToObject<User>(response.Content);

            return new Success<User>(user);
        }

        public async Task<ReadResponse<User>> GetUser(Guid userId)
        {
            await _httpClient.AddAuthHeader(_localStorageService);

            var response = await _httpClient.SendRequestAsync($"User/{userId}", HttpMethod.Get);

            var user = await JsonConverter.ToObject<User>(response.Content);

            return new Success<User>(user);
        }

        public async Task<UpdateResponse> EditUser(EditUserModel editModel)
        {
            var requestStringContent = await JsonConverter.ToStringContent(editModel);

            await _httpClient.AddAuthHeader(_localStorageService);

            var response = await _httpClient.SendRequestAsync($"User", HttpMethod.Patch, requestStringContent);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return await JsonConverter.ToValidationFailedList(response.Content);
            }

            return new Success();
        }
    }
}