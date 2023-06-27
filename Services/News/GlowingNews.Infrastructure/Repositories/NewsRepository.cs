using AWS.Shared.Client.Extensions;
using AWS.Shared.Client.GrpcServices;
using GlowingNews.Application.Common.Interfaces;
using GlowingNews.Application.Common.Models;
using GlowingNews.Application.DTOs;
using GlowingNews.Domain.Entities;
using GlowingNews.Domain.Enums;
using GlowingNews.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GlowingNews.Infrastructure.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private NewsContext _newsContext;

        public NewsRepository(NewsContext newsContext)
        {
            _newsContext = newsContext;
        }

        public async Task<List<NewsDailyDto>?> DailyNewsListAsync(List<Guid> userIdList)
        {
            //// Get the current date and time
            //var currentDate = DateTimeOffset.UtcNow;

            //// Calculate the start date and time for the last 24 hours
            //var startDate = currentDate.AddDays(-1);

            //// Query the News table to get the daily news for the given user IDs within the last 24 hours
            //var newsList = await _newsContext.News
            //    .Where(x => userIdList.Contains(x.UserId) && x.CreatedDate >= startDate && x.CreatedDate <= currentDate)
            //    .OrderByDescending(x => x.CreatedDate)
            //    .Select(x => new NewsDailyDto()
            //    {
            //        Id = x.Id,
            //        CreatedDate = x.CreatedDate
            //    })
            //    .ToListAsync();

            var newsList = await _newsContext.News.Select(x => new NewsDailyDto()
            {
                Id = x.Id,
                CreatedDate = x.CreatedDate
            }).ToListAsync();

            return newsList;
        }

        public async Task<NewsDto?> GetNewsAsync(Guid newsId)
        {
            var news = await _newsContext.News
                .Where(x => x.Id == newsId)
                .Select(x => new NewsDto()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    UserName = x.UserName,
                    MediaPath = string.IsNullOrWhiteSpace(x.MediaPath) ? "" : AwsFile.GetUrl(x.MediaPath),
                    MediaType = (MediaTypeDto)x.MediaType,
                    Text = x.Text,
                    CreatedDate = x.CreatedDate
                })
                .SingleOrDefaultAsync();
            return news;
        }

        public async Task<List<NewsDto>?> NewsListAsync(Guid userId)
        {
            var newsList = await _newsContext.News
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => new NewsDto()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    UserName = x.UserName,
                    MediaPath = string.IsNullOrWhiteSpace(x.MediaPath) ? "" : AwsFile.GetUrl(x.MediaPath),
                    MediaType = (MediaTypeDto)x.MediaType,
                    Text = x.Text,
                    CreatedDate = x.CreatedDate
                })
                .ToListAsync();

            return newsList;
        }

        public async Task<Guid> AddNewsAsync(AddNewsDto addNewsDto)
        {
            var news = new New()
            {
                Id = Guid.NewGuid(),
                UserId = addNewsDto.UserId,
                UserName = addNewsDto.UserName,
                Text = addNewsDto.Text,
                MediaPath = addNewsDto.MediaPath,
                MediaType = (MediaType)addNewsDto.MediaType
            };

            await _newsContext.News.AddAsync(news);
            await _newsContext.SaveChangesAsync();

            return news.Id;
        }

        public async Task<Guid?> EditNewsAsync(EditNewsDto editNewsDto)
        {
            var news = await _newsContext.News.SingleOrDefaultAsync(x => x.UserId == editNewsDto.UserId && x.Id == editNewsDto.NewsId);
            if (news != null)
            {
                news.Text = editNewsDto.Text;
                news.ModifiedDate = DateTimeOffset.UtcNow;

                _newsContext.News.Update(news);

                await _newsContext.SaveChangesAsync();

                return news.Id;
            }

            return null;
        }

        public async Task<string?> DeleteNewsAsync(Guid userId, Guid newsId)
        {
            var news = await _newsContext.News.SingleOrDefaultAsync(x => x.UserId == userId && x.Id == newsId);
            string? newsPath = null;
            if (news != null)
            {
                if (news.MediaPath != null)
                {
                    newsPath = news.MediaPath;
                }
                _newsContext.Remove(news);
                await _newsContext.SaveChangesAsync();
            }
            return newsPath;
        }
    }
}
