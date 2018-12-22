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
        private readonly IAccountService _accountService;
        private Regex loginRegex;
        private Regex passwordRegex;

        public AccountController(IUserService userService, IAccountService accountService)
        {
            _userService = userService;
            _accountService = accountService;
            loginRegex = new Regex(AccountRegex.Login);
            passwordRegex = new Regex(AccountRegex.Password);
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

            if (command.Login == null || command.Password == null)
            {
                ViewBag.ShowMessage = true;
                ViewBag.Message = "Fields can't be empty!";
                return View();
            }
            else if (!loginRegex.Match(command.Login).Success || !passwordRegex.Match(command.Password).Success)
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
            if (!ModelState.IsValid)
            {
                ViewBag.ShowMessage = true;
                ViewBag.Message = "Something went wrong";
                return View();
            }

            if (command.Login == null || command.Password == null)
            {
                ViewBag.ShowMessage = true;
                ViewBag.Message = "Fields can't be empty!";
                return View();
            }
            else if (!loginRegex.Match(command.Login).Success || !passwordRegex.Match(command.Password).Success)
            {
                ViewBag.ShowMessage = true;
                ViewBag.Message = "Wrong login or password";
                return View();
            }

            try
            {
                var account = await _accountService.LoginAccountAsync(command);
                HttpContext.Session.SetString("Login", account.Login);
                HttpContext.Session.SetString("IsAdmin", account.IsAdmin.ToString());

                if (account.IsAdmin == false)
                {
                    TempData["LoggedIn"] = true;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Profile", "Account");
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

        #region Admin

        [HttpGet("profile")]
        public async Task<IActionResult> AdminProfile()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}