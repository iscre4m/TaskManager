using Microsoft.AspNetCore.Mvc;
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
            if (_context.Users.Any(u => u.Login == user.Login))
            {
                ViewBag.Message = "Пользователь уже существует";

                return View("Error");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Redirect("/Home/Index");
        }

        [HttpPost]
        public IActionResult SignIn(User user)
        {
            if (!_context.Users.Any(u => u.Login == user.SignInLogin))
            {
                ViewBag.Message = "Пользователь не найден";

                return View("Error");
            }

            if (_context.Users.First(u => u.Login == user.SignInLogin).Password == user.SignInPassword)
            {
                return Redirect("/Home/Index");
            }

            ViewBag.Message = "Неверный пароль";

            return View("Error");
        }
    }
}