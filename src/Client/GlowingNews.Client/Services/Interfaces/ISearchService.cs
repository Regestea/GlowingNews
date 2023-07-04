using GlowingNews.Client.responses;
using GlowingNews.Client.responses.Base;

namespace GlowingNews.Client.Services.Interfaces
{
    public interface ISearchService
    {
        Task<ReadResponse<List<NewsSearch>>> SearchNews(string keyWord);
    }
}
