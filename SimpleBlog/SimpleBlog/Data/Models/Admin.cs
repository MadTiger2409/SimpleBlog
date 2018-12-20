using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Data.Models
{
    public class Admin : Account
    {
        public List<Post> Posts { get; set; }

        public Admin(byte[] passwordHash, byte[] salt, string login) : base(passwordHash, salt, login)
        {
            IsAdmin = true;
        }
    }
}
