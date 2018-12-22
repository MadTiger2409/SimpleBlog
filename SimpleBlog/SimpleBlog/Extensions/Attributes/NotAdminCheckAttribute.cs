using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SimpleBlog.Extensions.Attributes
{
    public class NotAdminCheckAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Session.GetString("IsAdmin") == null ||
                context.HttpContext.Session.GetString("IsAdmin").ToLowerInvariant() == "false")
            {
                context.Result = new RedirectResult("/");
            }
        }
    }
}
