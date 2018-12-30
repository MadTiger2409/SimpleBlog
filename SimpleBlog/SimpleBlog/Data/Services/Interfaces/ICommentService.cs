using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleBlog.Commands.Comment;
using SimpleBlog.Data.Models;

namespace SimpleBlog.Data.Services.Interfaces
{
    public interface ICommentService
    {
        Task CreateCommentAsync(CreateCommentCommand command, User user, Post post);
    }
}
