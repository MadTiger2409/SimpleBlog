using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Data.Database;
using SimpleBlog.Data.Services.Interfaces;
using SimpleBlog.Extensions.PdfRaport;

namespace SimpleBlog.Data.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly BlogContext _context;

        public StatisticsService(BlogContext context)
        {
            _context = context;
        }

        public async Task<RaportStatistics> GetStatisticsAsync()
        {
            var statistics = new RaportStatistics();

            statistics.PostsCount = await Task.FromResult(_context.Posts.Count());
            statistics.CommentsCount = await Task.FromResult(_context.Comments.Count());
            statistics.OldestPostDate = await _context.Posts.OrderBy(x => x.CreatedAt).Select(x => x.CreatedAt).FirstOrDefaultAsync();

            return statistics;
        }
    }
}
