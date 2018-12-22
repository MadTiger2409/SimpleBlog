using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Data.Models
{
    public class Message : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }

        public Message(string name, string email, string text) : base()
        {
            Name = name;
            Email = email;
            Text = text;
        }
    }
}
