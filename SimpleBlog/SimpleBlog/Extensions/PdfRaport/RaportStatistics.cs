using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Extensions.PdfRaport
{
    public class RaportStatistics
    {
        public int PostsCount { get; set; }
        public int CommentsCount { get; set; }
        public DateTime OldestPostDate { get; set; }
    }
}
