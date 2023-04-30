using System.Net;
using System.Text.Json;
using Blazored.LocalStorage;
using GlowingNews.Client.DTOs.Error;
using GlowingNews.Client.Extensions;
using GlowingNews.Client.Models.Auth;
using GlowingNews.Client.responses.Base;
using GlowingNews.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using OneOf;
using OneOf.Types;

namespace GlowingNews.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthenticationStateProvider _authStateProvider;
        private HttpClient _httpClient;
        private ILocalStorageService _localStorageService;

        public AuthService(AuthenticationStateProvider authStateProvider, IHttpClientFactory httpClientFactory,
            ILocalStorageService localStorageService)
        {
            _authStateProvider = authStateProvider;
            _httpClient = httpClientFactory.CreateClient(nameof(Enums.HttpClients.IdentityServer));
            _localStorageService = localStorageService;
        }


        public async Task<CreateResponse> Register(RegisterModel request)
        {
            var requestStringContent = await JsonConverter.ToStringContent(request);

            var response = await _httpClient.SendRequestAsync("Register", HttpMethod.Post, requestStringContent);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return new Success<Guid>(Guid.Parse(response.Content.Trim('"')));
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return await JsonConverter.ToValidationFailedList(response.Content);
            }

            return new Error<string>("unknown issue");
        }

        public async Task<ReadResponse<string>> Login(LoginModel request)
        {
            var requestStringContent = await JsonConverter.ToStringContent(request);

            var response = await _httpClient.SendRequestAsync("Login", HttpMethod.Post, requestStringContent);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return new Success<string>(response.Content);
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return await JsonConverter.ToValidationFailedList(response.Content);
            }

            return new NotFound();
        }

        public async Task<bool> IsUserAuthenticated()
        {
            var userIdentity = (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity;
            return userIdentity != null && userIdentity.IsAuthenticated;
        }
    }
}