using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlowingNews.Application.Common.Models;
using GlowingNews.Application.DTOs;

namespace GlowingNews.Application.Common.Interfaces
{
    public interface INewsRepository
    {
        Task<List<NewsDailyDto>?> DailyNewsListAsync(List<Guid> userIdList);

        Task<NewsDto?> GetNewsAsync(Guid newsId);

        Task<List<NewsDto>?> NewsListAsync(Guid userId);

        Task<Guid> AddNewsAsync(AddNewsDto newsDto);

        Task<Guid?> EditNewsAsync(EditNewsDto newsDto);

        Task<string?> DeleteNewsAsync(Guid userId,Guid newsId);
    }
}
