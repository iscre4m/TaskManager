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
            User user = await _context.Users.FirstOrDefaultAsync(
                u => u.Username == model.Username
                  && u.Password == model.Password);

            if (user is not null)
            {
                await Authenticate(model.Username);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);

            if(user is null)
            {
                _context.Users.Add(new User { Username = model.Username, Password = model.Password });
                await _context.SaveChangesAsync();

                await Authenticate(model.Username);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Пользователь уже существует");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");
        }
    }
}