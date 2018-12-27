using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleBlog.Commands.Message;
using SimpleBlog.Data.Models;

namespace SimpleBlog.Data.Services.Interfaces
{
    public interface IMessageService
    {
        Task AddMessageAsync(CreateMessageCommand command);
        Task<IEnumerable<Message>> GetMessagessAsync();
    }
}
