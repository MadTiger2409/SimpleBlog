using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Data.Models
{
    public class Admin : Entity
    {
        public string Login { get; set; }
        public byte[] Salt { get; set; }
        public byte[] PasswordHash { get; set; }

        public List<Post> Posts { get; set; }
    }
}
