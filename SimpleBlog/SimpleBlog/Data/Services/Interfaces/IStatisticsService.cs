using SimpleBlog.Extensions.PdfRaport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Data.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task<RaportStatistics> GetStatisticsAsync();
    }
}
