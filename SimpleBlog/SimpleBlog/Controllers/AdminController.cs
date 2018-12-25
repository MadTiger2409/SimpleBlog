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
            => await Task.FromResult(View());

        [HttpGet("all-posts")]
        public async Task<IActionResult> AllPosts()
            => await Task.FromResult(View());

        [HttpGet("new-post")]
        public async Task<IActionResult> NewPost()
            => await Task.FromResult(View());
    }
}