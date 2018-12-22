using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Commands.Message;
using SimpleBlog.Data.Services.Interfaces;

namespace SimpleBlog.Controllers
{
    public class HomeController : Controller
    {
        private IPostService _postService;

        public HomeController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetPostsAsync();
            ViewBag.Registered = TempData["Registered"];
            ViewBag.LoggedIn = TempData["LoggedIn"];

            return View(posts);
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpPost("contact")]
        public async Task<IActionResult> Contact(CreateMessageCommand command)
        {
            throw new NotImplementedException();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
