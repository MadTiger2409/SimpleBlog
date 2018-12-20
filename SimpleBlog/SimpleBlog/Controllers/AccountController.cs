using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Commands.Account;
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

        #region Register
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
                TempData["Registered"] = true;
                return RedirectToAction("Index", "Home");
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
        #endregion

        #region LogIn
        [HttpGet("login")]
        public IActionResult LogIn()
        {
            if (HttpContext.Session.GetString("Login") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn(LogInCommand command)
        {
            // get account (from Accounts DbSet), check if it exits and if credentials are good
            // if so, then check if it is admin and return view or redirect with information
            // base in this fact
            throw new NotImplementedException();
        }
        #endregion
    }
}