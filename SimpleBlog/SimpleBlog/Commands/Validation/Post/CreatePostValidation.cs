using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SimpleBlog.Commands.Post;
using SimpleBlog.Extensions;
using SimpleBlog.Extensions.Regex;

namespace SimpleBlog.Commands.Validation.Post
{
    public static class CreatePostValidation
    {
        private static Regex titleRegex = new Regex(PostRegex.Title);
        private static Regex descriptionRegex = new Regex(PostRegex.Description);
        private static Regex tagsRegex = new Regex(PostRegex.Tags);

        public static void CommandValidation(CreatePostCommand command)
        {
            if (command.Title == null || command.Description == null || command.Content == null || command.Tags == null)
            {
                throw new InternalSystemException("Fields can't be empty!");
            }
            else if (!titleRegex.IsMatch(command.Title) || !descriptionRegex.IsMatch(command.Description) ||
                (command.Content.Length < 10 || command.Content.Length > 2000) || !tagsRegex.IsMatch(command.Tags))
            {
                throw new InternalSystemException("Please provide correct data!");
            }
        }
    }
}
