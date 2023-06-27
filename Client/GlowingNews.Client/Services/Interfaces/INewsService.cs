using GlowingNews.Client.Models.News;
using GlowingNews.Client.responses;
using GlowingNews.Client.responses.Base;

namespace GlowingNews.Client.Services.Interfaces
{
    public interface INewsService
    {
        Task<CreateResponse<IdResult>> AddNews(AddNewsModel newsModel);
        Task<UpdateResponse> EditNews(Guid newsId,EditNewsModel newsModel);
        Task<DeleteResponse> DeleteNews(Guid newsId);
        Task<ReadResponse<News>> GetNews(Guid id);
        Task<ReadResponse<List<News>>> GetNewsList(Guid userId);
        Task<ReadResponse<List<NewsDaily>>> GetDailyNewsList(List<Guid> followingIdList);
    }
}
