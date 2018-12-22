using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Extensions.Attributes;

namespace SimpleBlog.Controllers
{
    [NotAdminCheck]
    public class AdminController : Controller
    {
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            return await Task.FromResult(View());
        }
    }
}