using System.Collections;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Text;
using Blazored.LocalStorage;
using GlowingNews.Client.DTOs.Response;
using Microsoft.AspNetCore.Components.Forms;

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

        public static async Task<HttpResponseDto> SendRequestAsync(this HttpClient httpClient, string url, IBrowserFile uploadFile, string fieldName, Action<UploadPercentageDto> onProgress)
        {
            var content = new MultipartFormDataContent();

            using (var fileStream = uploadFile.OpenReadStream(100000000))
            {
                // and the 40096 which are 40KB per packet and the third argument which as a callback for the OnProgress event (u, p) are u = Uploaded bytes and P is the percentage
                var streamContent = new ProgressiveStreamContent(fileStream, 40096, onProgress.Invoke);

                // Add the streamContent with the name to the FormContent variable
                content.Add(streamContent, fieldName, uploadFile.Name);

                // Submit the request
                var response = await httpClient.PostAsync(url, content);
                var responseDto = new HttpResponseDto()
                {
                    StatusCode = response.StatusCode,
                    Content = await response.Content.ReadAsStringAsync()
                };
                return responseDto;
            }
        }
    }
}