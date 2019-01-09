using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Commands.Message
{
    public class CreateMessageCommand
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public string Answer { get; set; }
    }
}
