using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Extensions
{
    public class InternalSystemException : Exception
    {
        public InternalSystemException() { }
        public InternalSystemException(string message) : base(message) { }
        public InternalSystemException(string message, Exception inner) : base(message, inner) { }
    }
}
