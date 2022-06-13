using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly TaskManagerContext _context;

        public AccountController(TaskManagerContext context)
        {
            _context = context;
        }

        public async Task Authenticate(string username)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, username)
            };

            ClaimsIdentity id = new(claims, "ApplicationCookie",
                                    ClaimsIdentity.DefaultNameClaimType,
                                    ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            await Authenticate(model.Username);

            return RedirectToAction("App", "Main");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            _context.Users.Add(new()
            {
                Username = model.Username,
                Password = model.Password
            });
            await _context.SaveChangesAsync();

            await Authenticate(model.Username);

            return RedirectToAction("App", "Main");
        }

        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        //    return RedirectToAction("Login", "Account");
        //}
    }
}