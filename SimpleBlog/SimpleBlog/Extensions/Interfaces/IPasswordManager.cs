using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Extensions.Interfaces
{
    public interface IPasswordManager
    {
        void CalculatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        void CalculatePasswordHash(string password, byte[] passwordSalt, out byte[] passwordHash);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
