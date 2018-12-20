using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Data.Models
{
    public abstract class Account : Entity
    {
        public string Login { get; set; }
        public byte[] Salt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool IsAdmin { get; set; }

        public Account(byte[] passwordHash, byte[] salt, string login)
        {
            PasswordHash = passwordHash;
            Salt = salt;
            Login = login;
        }
    }
}
