﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Commands.Account
{
    public class CreateUserCommand
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Answer { get; set; }
    }
}
