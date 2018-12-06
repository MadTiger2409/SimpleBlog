using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Commands.User;
using SimpleBlog.Data.Services.Interfaces;
using SimpleBlog.Extensions;

namespace SimpleBlog.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("register")]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registration(CreateUserCommand command)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ShowMessage = true;
                ViewBag.Message = "Something went wrong";
                return View();
            }

            Regex loginRegex = new Regex(AccountRegex.Login);
            Regex passwordRegex = new Regex(AccountRegex.Password);

            if (!loginRegex.Match(command.Login).Success || !passwordRegex.Match(command.Password).Success)
            {
                ViewBag.ShowMessage = true;
                ViewBag.Message = "Wrong login or password";
                return View();
            }

            try
            {
                await _userService.RegisterUserAsync(command);

                ViewBag.ShowMessage = true;
                ViewBag.Message = "User registered";
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
                ViewBag.Message = "Something went wrong";
                return View();
            }
        }
    }
}