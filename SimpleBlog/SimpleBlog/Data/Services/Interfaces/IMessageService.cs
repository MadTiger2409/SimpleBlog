using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleBlog.Commands.Message;

namespace SimpleBlog.Data.Services.Interfaces
{
    public interface IMessageService
    {
        Task AddMessageAsync(CreateMessageCommand command);
    }
}
