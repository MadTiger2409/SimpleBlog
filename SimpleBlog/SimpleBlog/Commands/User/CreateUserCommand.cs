using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Commands.User
{
    public class CreateUserCommand
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
