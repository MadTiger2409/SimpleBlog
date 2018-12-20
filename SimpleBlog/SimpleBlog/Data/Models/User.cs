using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Data.Models
{
    public class User : Account
    {
        public List<Comment> Comments { get; set; }

        public User(byte[] passwordHash, byte[] salt, string login) : base(passwordHash, salt, login)
        {
            IsAdmin = false;
        }
    }
}
