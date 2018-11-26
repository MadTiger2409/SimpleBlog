using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Commands.User;

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
            return View();
        }
    }
}