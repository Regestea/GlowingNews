using System.Net;

namespace GlowingNews.Client.DTOs.Response
{
    public class HttpResponseDto
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Content { get; set; } = null!;
    }
}