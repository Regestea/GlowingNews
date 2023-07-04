﻿using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace GlowingNews.Client.Extensions
{
    public static class HttpClientAuthorizationHeaderExtension
    {
        public static async Task<HttpClient> AddAuthHeader(this HttpClient httpClient,
            ILocalStorageService localStorageService)
        {
            var authToken = await localStorageService.GetItemAsStringAsync("authToken");
            //var normalizedToken = authToken.Replace("\"", "");
           
            if (!string.IsNullOrEmpty(authToken))
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", authToken);
                }
                catch
                {
                    await localStorageService.RemoveItemAsync("authToken");
                }
            }

            return httpClient;
        }
    }
}