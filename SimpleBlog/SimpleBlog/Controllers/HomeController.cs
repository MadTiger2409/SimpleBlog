using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Commands.Message;
using SimpleBlog.Commands.Validation.Message;
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
            if (!ModelState.IsValid)
            {
                ViewBag.ShowMessage = true;
                ViewBag.Message = "Something went wrong";
                return View();
            }

            try
            {
                CreateMessageValidator.CommandValidation(command);

                await _messageService.AddMessageAsync(command);
                ViewBag.Added = true;
                return View();
            }
            catch (InternalSystemException ex)
            {
                ViewBag.ShowMessage = true;
                ViewBag.Message = ex.Message;
                return View();
            }
            catch (Exception)
            {
                ViewBag.ShowMessage = true;
                ViewBag.Message = "Something went wrong!";
                return View();
            }
        }

        [HttpGet("post/{id}")]
        public async Task<IActionResult> Post(string id)
        {
            try
            {
                var postId = int.Parse(id);
                var post = await _postService.GetPostAsync(postId);
                return View(post);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
