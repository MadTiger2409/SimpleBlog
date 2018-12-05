using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Commands.User;
using SimpleBlog.Extensions;

namespace SimpleBlog.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        [HttpGet("register")]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Registration(CreateUserCommand command)
        {
            Regex loginRegex = new Regex(AccountRegex.Login);
            Regex passwordRegex = new Regex(AccountRegex.Password);

            if (!loginRegex.Match(command.Login).Success || !passwordRegex.Match(command.Password).Success)
            {
                ViewBag.ShowMessage = true;
                ViewBag.Message = "Wrong login or password";
                return View();
            }

            return View();
        }
    }
}