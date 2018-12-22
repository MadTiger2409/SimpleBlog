using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleBlog.Data.Models;
using SimpleBlog.Extensions;

namespace SimpleBlog.Data.Database
{
    public static class DataSeeder
    {
        public static void Initialize(BlogContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Admins.Any())
            {
                var password = "Adm1n.P@ss";
                byte[] passwordHash, passwordSalt;
                var passwordManager = new PasswordManager();

                passwordManager.CalculatePasswordHash(password, out passwordHash, out passwordSalt);
                context.Admins.Add(new Admin(passwordHash, passwordSalt, "SuperAdmin666"));
                context.SaveChanges();
            }
        }
    }
}
