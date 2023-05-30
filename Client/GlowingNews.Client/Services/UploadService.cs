using GlowingNews.Client.Models.File;
using GlowingNews.Client.responses;
using GlowingNews.Client.responses.Base;
using GlowingNews.Client.Services.Interfaces;
using OneOf.Types;
using System.Net;
using GlowingNews.Client.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Forms;
using GlowingNews.Client.DTOs.Response;

namespace GlowingNews.Client.Services
{
    public class UploadService: IUploadService
    {
        ILocalStorageService _localStorageService;
        private HttpClient _httpClient;

        public UploadService(IHttpClientFactory httpClientFactory, ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;

            _httpClient = httpClientFactory.CreateClient(nameof(Enums.HttpClients.Aws));
        }

        public async Task<CreateResponse<JwtToken>> UserProfileImage(ImageUploadModel request, Action<UploadPercentageDto> onProgress)
        {
            await _httpClient.AddAuthHeader(_localStorageService);
            var response = await _httpClient.SendRequestAsync("Aws/UserProfileImage", request.Image, "Image", onProgress.Invoke);

           var token=await JsonConverter.ToObject<JwtToken>(response.Content);

            if (response.StatusCode==HttpStatusCode.OK)
            {
                return new Success<JwtToken>(token);
            }

            return new Error<string>("upload field");
        }

        public async Task<CreateResponse<JwtToken>> NewsImage(ImageUploadModel request, Action<UploadPercentageDto> onProgress)
        {
            await _httpClient.AddAuthHeader(_localStorageService);
            var response = await _httpClient.SendRequestAsync("Aws/NewsImage", request.Image, "Image", onProgress.Invoke);

            var token = await JsonConverter.ToObject<JwtToken>(response.Content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return new Success<JwtToken>(token);
            }

            return new Error<string>("upload field");
        }

        public async Task<CreateResponse<JwtToken>> NewsVideo(VideoUploadModel request, Action<UploadPercentageDto> onProgress)
        {
            await _httpClient.AddAuthHeader(_localStorageService);
            var response = await _httpClient.SendRequestAsync("Aws/NewsVideo", request.Video, "Video", onProgress.Invoke);

            var token = await JsonConverter.ToObject<JwtToken>(response.Content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return new Success<JwtToken>(token);
            }

            return new Error<string>("upload field");
        }
    }
}
