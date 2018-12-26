using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Extensions.Regex
{
    public static class PostRegex
    {
        public static string Title { get; } = @"^.+( .+)*$";
        public static string Description { get; } = @"^.+( .+)*$";
        public static string Tags { get; } = @"^[a-z\-\;]+([a-z\-\;]+)*$";
    }
}
