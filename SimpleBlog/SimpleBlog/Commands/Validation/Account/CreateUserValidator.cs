﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SimpleBlog.Commands.Account;
using SimpleBlog.Extensions;
using SimpleBlog.Extensions.Regex;

namespace SimpleBlog.Commands.Validation.Account
{
    public static class CreateUserValidator
    {
        private static Regex loginRegex = new Regex(AccountRegex.Login);
        private static Regex passwordRegex = new Regex(AccountRegex.Password);

        public static void CommandValidation(CreateUserCommand command)
        {
            if (command.Login == null || command.Password == null)
            {
                throw new InternalSystemException("Fields can't be empty!");
            }
            else if (!loginRegex.IsMatch(command.Login) || !passwordRegex.IsMatch(command.Password))
            {
                throw new InternalSystemException("Wrong login or password");
            }
        }
    }
}
