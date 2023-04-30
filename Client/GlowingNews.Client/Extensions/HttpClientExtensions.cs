using System.Collections;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Text;
using Blazored.LocalStorage;
using GlowingNews.Client.DTOs.Response;

namespace GlowingNews.Client.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseDto> SendRequestAsync(this HttpClient httpClient, string url,
            HttpMethod method, StringContent? jsonBody = null)
        {
            using var request = new HttpRequestMessage(method, url);

            if (jsonBody != null)
            {
                request.Content = jsonBody;
            }

            using var response = await httpClient.SendAsync(request);

            var responseDto = new HttpResponseDto()
            {
                StatusCode = response.StatusCode,
                Content = await response.Content.ReadAsStringAsync()
            };
            return responseDto;
        }

        public static async Task<HttpResponseDto> SendRequestAsync(this HttpClient httpClient, string url,
            HttpMethod method, IFormFile uploadFile, string fieldName)
        {
            using var request = new HttpRequestMessage(method, url);
            request.Content = new MultipartFormDataContent
            {
                { new StreamContent(uploadFile.OpenReadStream()), fieldName, uploadFile.FileName }
            };
            using var response = await httpClient.SendAsync(request);
            var responseDto = new HttpResponseDto()
            {
                StatusCode = response.StatusCode,
                Content = await response.Content.ReadAsStringAsync()
            };
            return responseDto;
        }
    }
}