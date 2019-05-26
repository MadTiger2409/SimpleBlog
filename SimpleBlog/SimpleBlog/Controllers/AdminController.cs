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
        private readonly IMessageService _messageService;

        public AdminController(IPostService postService, IAdminService adminService, IMessageService messageService)
        {
            _postService = postService;
            _adminService = adminService;
            _messageService = messageService;
        }

        [HttpGet("messages")]
        public async Task<IActionResult> Messages()
        {
            var messages = await _messageService.GetMessagessAsync();

            return View(messages);
        }

        [HttpGet("new-post")]
        public async Task<IActionResult> NewPost() => await Task.FromResult(View());

        [HttpPost("new-post")]
        public async Task<IActionResult> NewPost(CreatePostCommand command)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ShowMessage = true;
                ViewBag.Message = "Something went wrong";
                return View();
            }

            try
            {
                CreatePostValidation.CommandValidation(command);

                var admin = await _adminService.GetAdminAsync();
                await _postService.AddPostAsync(command, admin);

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

        [HttpGet("posts")]
        public async Task<IActionResult> Posts()
        {
            var posts = await _postService.GetPostsAsync();

            return View(posts);
        }

        [HttpGet("delete-post/{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Posts");
            }

            try
            {
                await _postService.DeleteAsync(id);
                return RedirectToAction("Posts");
            }
            catch (Exception)
            {
                return RedirectToAction("Posts");
            }
        }

        [HttpGet("edit-post/{id}")]
        public async Task<IActionResult> EditPost(int id)
        {
            var post = await _postService.GetPostAsync(id);

            if (post == null)
                return RedirectToAction("Posts");

            var command = new UpdatePostCommand(post);
            return View(command);
        }

        [HttpPost("edit-post")]
        public async Task<IActionResult> EditPost(UpdatePostCommand command)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ShowMessage = true;
                ViewBag.Message = "Something went wrong";
                return View();
            }

            try
            {
                CreatePostValidation.CommandValidation(command);

                var admin = await _adminService.GetAdminAsync();
                await _postService.EditPostAsync(command);

                return RedirectToAction("Posts");
            }
            catch (InternalSystemException ex)
            {
                ViewBag.ShowMessage = true;
                ViewBag.Message = ex.Message;
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Posts");
            }
        }
    }
}