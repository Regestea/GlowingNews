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

        public async Task<NewsDto?> LastNewsAsync(Guid userId)
        {
            var news = await _newsContext.News
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => new NewsDto()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    UserName = x.UserName,
                    MediaPath = string.IsNullOrWhiteSpace(x.MediaPath) ? "" : AwsFile.GetUrl(x.MediaPath),
                    MediaType = (MediaTypeDto)x.MediaType,
                    Text = x.Text
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
                    Text = x.Text
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
            string? newsPath=null;
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
