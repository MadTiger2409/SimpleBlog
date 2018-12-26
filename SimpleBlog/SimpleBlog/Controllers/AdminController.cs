using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Commands.Post;
using SimpleBlog.Commands.Validation.Post;
using SimpleBlog.Data.Services.Interfaces;
using SimpleBlog.Extensions;
using SimpleBlog.Extensions.Attributes;

namespace SimpleBlog.Controllers
{
    [NotAdminCheck]
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly IPostService _postService;
        private readonly IAdminService _adminService;

        public AdminController(IPostService postService, IAdminService adminService)
        {
            _postService = postService;
            _adminService = adminService;
        }

        [HttpGet("messages")]
        public async Task<IActionResult> Messages()
            => await Task.FromResult(View());

        [HttpGet("all-posts")]
        public async Task<IActionResult> AllPosts()
            => await Task.FromResult(View());

        [HttpGet("new-post")]
        public async Task<IActionResult> NewPost()
            => await Task.FromResult(View());

        [HttpPost("new-post")]
        public async Task<IActionResult> NewPost(CreatePostCommand command)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ShowMessage = true;
                ViewBag.Message = "Something went wrong";
                return View();
            }

            //try
            //{
                CreatePostValidation.CommandValidation(command);

                var admin = await _adminService.GetAdminAsync();
                await _postService.AddPostAsync(command, admin);

                ViewBag.Added = true;
                return View();
            //}
            //catch (InternalSystemException ex)
            //{
            //    ViewBag.ShowMessage = true;
            //    ViewBag.Message = ex.Message;
            //    return View();
            //}
            //catch (Exception)
            //{
            //    ViewBag.ShowMessage = true;
            //    ViewBag.Message = "Something went wrong!";
            //    return View();
            //}
        }
    }
}