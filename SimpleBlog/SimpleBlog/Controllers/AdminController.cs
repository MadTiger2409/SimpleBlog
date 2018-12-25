using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Extensions.Attributes;

namespace SimpleBlog.Controllers
{
    [NotAdminCheck]
    [Route("admin")]
    public class AdminController : Controller
    {
        [HttpGet("statistics")]
        public async Task<IActionResult> Statistics()
        {
            return await Task.FromResult(View());
        }
    }
}