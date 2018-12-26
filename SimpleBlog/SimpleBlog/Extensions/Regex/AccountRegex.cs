using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Extensions.Regex
{
    public static class AccountRegex
    {
        public static string Login { get; } = @"[a-zA-Z][a-zA-Z0-9]{5,20}";
        public static string Password { get; } = @"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,30}";
    }
}
