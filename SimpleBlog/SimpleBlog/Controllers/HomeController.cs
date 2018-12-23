using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Commands.Message;
using SimpleBlog.Data.Services.Interfaces;
using SimpleBlog.Extensions;

namespace SimpleBlog.Controllers
{
    public class HomeController : Controller
    {
        private IPostService _postService;
        private IMessageService _messageService;

        public HomeController(IPostService postService, IMessageService messageService)
        {
            _postService = postService;
            _messageService = messageService;
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
            var nameRegex = new Regex(MessageRegex.Name);
            var emailRegex = new Regex(MessageRegex.Email);
            var textRegex = new Regex(MessageRegex.Text);

            if (command.Name == null || command.Email == null || command.Text == null)
            {
                ViewBag.ShowMessage = true;
                ViewBag.Message = "Fields can't be empty!";
                return View();
            }
            else if (!nameRegex.IsMatch(command.Name) || !emailRegex.IsMatch(command.Email) || !textRegex.IsMatch(command.Text))
            {
                ViewBag.ShowMessage = true;
                ViewBag.Message = "Please provide correct data!";
                return View();
            }

            try
            {
                await _messageService.AddMessageAsync(command);

                ViewBag.Added = true;
                return View();
            }
            catch (Exception)
            {
                ViewBag.ShowMessage = true;
                ViewBag.Message = "Something went wrong!";
                return View();
            }
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
