using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleBlog.Commands.Account;
using SimpleBlog.Data.Database;
using SimpleBlog.Data.Models;
using SimpleBlog.Data.Services.Interfaces;
using SimpleBlog.Extensions;
using SimpleBlog.Extensions.Interfaces;

namespace SimpleBlog.Data.Services
{
    public class AccountService : IAccountService
    {
        private readonly BlogContext _context;
        private readonly IPasswordManager _passwordManager;

        public AccountService(BlogContext context, IPasswordManager passwordManager)
        {
            _context = context;
            _passwordManager = passwordManager;
        }

        public async Task<Account> LoginAccountAsync(LogInCommand command)
        {
            var account = await Task.FromResult(_context.Accounts.SingleOrDefault(x => x.Login.ToLowerInvariant() ==
                command.Login.ToLowerInvariant()));

            if (account == null)
            {
                throw new InternalSystemException("There is no account with given credentials.");
            }

            var isPasswordCorrect = _passwordManager.VerifyPasswordHash(command.Password, account.PasswordHash, account.Salt);

            if (isPasswordCorrect == false)
            {
                throw new InternalSystemException("Wrong credentials.");
            }

            return account;
        }
    }
}
