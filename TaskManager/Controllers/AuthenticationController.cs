using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly TaskManagerContext _context;

        public AuthenticationController(TaskManagerContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(User user)
        {
            if (await _context.Users.AnyAsync(u => u.Login == user.Login))
            {
                ViewBag.Message = "Пользователь уже существует";

                return View("Error");
            }

            user.IsSignedIn = true;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("App", "Main");
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(User user)
        {
            User queryUser = await _context.Users.FirstOrDefaultAsync(u => u.Login == user.SignInLogin);

            if (queryUser is not null)
            {
                if (queryUser.Password == user.SignInPassword)
                {
                    queryUser.IsSignedIn = true;
                    await _context.SaveChangesAsync();

                    return RedirectToAction("App", "Main");
                }

                ViewBag.Message = "Неверный пароль";

                return View("Error");
            }

            ViewBag.Message = "Пользователь не найден";

            return View("Error");
        }

        public new async Task<IActionResult> SignOut()
        {
            _context.Users.First(u => u.IsSignedIn == true).IsSignedIn = false;
            await _context.SaveChangesAsync();

            return Redirect("/Home/Index");
        }
    }
}