using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWS.Application.Common.Interfaces;
using AWS.Application.Models;
using AWS.Domain.Enums;
using AWS.Infrastructure.Persistence.AWS;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AWS.Infrastructure.Persistence.Services
{
    public class AwsCleanerService : BackgroundService
    {
        private IServiceProvider _serviceProvider;

        public AwsCleanerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var scope = _serviceProvider.CreateScope();
                var _awsFileRepository = scope.ServiceProvider.GetRequiredService<IAwsFileRepository>();
                var _awsIndexDbContext = scope.ServiceProvider.GetRequiredService<AwsIndexDbContext>();
                var now = DateTimeOffset.UtcNow;

                var oneDayAgo = now.AddMinutes(-1);

                var filesToDelete = await _awsIndexDbContext.AwsFiles
                    .Where(f => f.HaveUse == false && f.CreatedDate <= oneDayAgo)
                    .ToListAsync(cancellationToken: stoppingToken);

                if (filesToDelete.Any())
                {
                    var profileFilesToDelete = filesToDelete.Where(x => x.Bucket == Bucket.profile)
                        .Select(x => x.FileName).ToList();
                    if (profileFilesToDelete.Any())
                    {
                        await _awsFileRepository.DeleteFilesAsync(new DeleteFilesModel()
                        { Bucket = Bucket.profile, FilesName = profileFilesToDelete });
                    }


                    var newsVideoFilesToDelete = filesToDelete.Where(x => x.Bucket == Bucket.newsvideo)
                        .Select(x => x.FileName).ToList();
                    if (newsVideoFilesToDelete.Any())
                    {
                        await _awsFileRepository.DeleteFilesAsync(new DeleteFilesModel()
                        { Bucket = Bucket.newsvideo, FilesName = newsVideoFilesToDelete });
                    }


                    var newsImageFilesToDelete = filesToDelete.Where(x => x.Bucket == Bucket.newsimage)
                        .Select(x => x.FileName).ToList();
                    if (newsImageFilesToDelete.Any())
                    {
                        await _awsFileRepository.DeleteFilesAsync(new DeleteFilesModel()
                        { Bucket = Bucket.newsimage, FilesName = newsImageFilesToDelete });
                    }

                    _awsIndexDbContext.AwsFiles.RemoveRange(filesToDelete);

                    await _awsIndexDbContext.SaveChangesAsync(stoppingToken);

                    _awsIndexDbContext.Dispose();
                }

                // Wait 24 hours before running the function again
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}