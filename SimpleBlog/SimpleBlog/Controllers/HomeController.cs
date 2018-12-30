using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SimpleBlog.Commands.Message;
using SimpleBlog.Commands.Validation.Message;
using SimpleBlog.Data.Services.Interfaces;
using SimpleBlog.Extensions;
using SimpleBlog.ViewModels;
using SimpleBlog.Commands.Comment;

namespace SimpleBlog.Controllers
{
    public class HomeController : Controller
    {
        private IPostService _postService;
        private IMessageService _messageService;
        private ICommentService _commentService;
        private IUserService _userService;

        public HomeController(IPostService postService, IMessageService messageService, ICommentService commentService,
            IUserService userService)
        {
            _postService = postService;
            _messageService = messageService;
            _commentService = commentService;
            _userService = userService;
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
            ViewBag.ShowMessage = TempData["CommentFlag"];
            ViewBag.Message = TempData["CommentMessage"];

            try
            {
                var postId = int.Parse(id);
                var post = await _postService.GetPostAsync(postId);
                var postViewModel = new PostDetailsViewModel(post);

                return View(postViewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost("post/{id}")]
        public async Task<IActionResult> CreateComment(int id, PostDetailsViewModel command)
        {
            if (string.IsNullOrWhiteSpace(command.NewComment.Content))
            {
                TempData["CommentFlag"] = true;
                TempData["CommentMessage"] = "Comment can't be empty!";
                return RedirectToAction("Post", "Home", id);
            }

            try
            {
                var userId = int.Parse(HttpContext.Session.GetString("Id"));
                var user = await _userService.GetUserAsync(userId);
                var post = await _postService.GetPostAsync(id);

                await _commentService.CreateCommentAsync(command.NewComment, user, post);

                return RedirectToAction("Post", "Home", id);
            }
            catch (InternalSystemException ex)
            {
                TempData["CommentFlag"] = true;
                TempData["CommentMessage"] = ex.Message;
                return RedirectToAction("Post", "Home", id);
            }
            catch (Exception)
            {
                TempData["CommentFlag"] = true;
                TempData["CommentMessage"] = "Something went wrong!";
                return RedirectToAction("Post", "Home", id);
            }
        }

        [Route("search")]
        public async Task<IActionResult> SearchPost(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                var posts = await _postService.GetPostsAsync(query);

                return View(posts);
            }
            catch (Exception)
            {
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
