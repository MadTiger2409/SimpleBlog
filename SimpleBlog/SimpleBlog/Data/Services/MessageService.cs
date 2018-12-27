using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleBlog.Commands.Message;
using SimpleBlog.Data.Database;
using SimpleBlog.Data.Models;
using SimpleBlog.Data.Services.Interfaces;

namespace SimpleBlog.Data.Services
{
    public class MessageService : IMessageService
    {
        private readonly BlogContext _context;

        public MessageService(BlogContext context)
        {
            _context = context;
        }

        public async Task AddMessageAsync(CreateMessageCommand command)
        {
            var message = new Message(command.Name, command.Email, command.Text);
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetMessagessAsync()
            => await Task.FromResult(_context.Messages.OrderByDescending(x => x.CreatedAt).AsEnumerable());
    }
}
