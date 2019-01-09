using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Commands.Account;
using SimpleBlog.Commands.Validation.Account;
using SimpleBlog.Data.Services.Interfaces;
using SimpleBlog.Extensions;
using SimpleBlog.Extensions.Attributes;

namespace SimpleBlog.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;

        public AccountController(IUserService userService, IAccountService accountService)
        {
            _userService = userService;
            _accountService = accountService;
        }

        #region Register
        [HttpGet("register")]
        [LoggedInCheck]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost("register")]
        [LoggedInCheck]
        public async Task<IActionResult> Registration(CreateUserCommand command)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ShowMessage = true;
                ViewBag.Message = "Something went wrong";
                return View();
            }

            if (string.IsNullOrEmpty(command.Answer) || command.Answer.ToLowerInvariant() != "martha")
            {
                ViewBag.ShowMessage = true;
                ViewBag.Message = "Answer isn't correct. Try again!";
                return View();
            }

            try
            {
                CreateUserValidator.CommandValidation(command);

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
        [LoggedInCheck]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost("login")]
        [LoggedInCheck]
        public async Task<IActionResult> LogIn(LogInCommand command)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ShowMessage = true;
                ViewBag.Message = "Something went wrong";
                return View();
            }

            try
            {
                LogInValidator.CommandValidation(command);

                var account = await _accountService.LoginAccountAsync(command);
                HttpContext.Session.SetString("Login", account.Login);
                HttpContext.Session.SetString("IsAdmin", account.IsAdmin.ToString());
                HttpContext.Session.SetString("Id", account.Id.ToString());

                if (account.IsAdmin == false)
                {
                    TempData["LoggedIn"] = true;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Messages", "Admin");
                }
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

        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            HttpContext.Session.Clear();

            return await Task.FromResult(RedirectToAction("Index", "Home"));
        }
    }
}