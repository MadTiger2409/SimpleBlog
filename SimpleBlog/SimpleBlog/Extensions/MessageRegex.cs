using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Extensions
{
    public static class MessageRegex
    {
        public static string Name { get; } = @"^[A-Za-z]\S{2,50}";
        public static string Email { get; } = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        public static string Text { get; } = @".{5,400}";
    }
}
