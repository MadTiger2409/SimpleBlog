using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleBlog.Extensions;

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

        public Account(string password, string login)
        {
            var manager = new PasswordManager();
            byte[] passwordHash, salt;

            manager.CalculatePasswordHash(password, out passwordHash, out salt);

            PasswordHash = passwordHash;
            Salt = salt;
            Login = login;
        }
    }
}
