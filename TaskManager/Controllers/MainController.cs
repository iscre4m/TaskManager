using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class MainController : Controller
    {
        private readonly TaskManagerContext _context;

        public MainController(TaskManagerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> App()
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.IsSignedIn == true);

            if (user is not null)
            {
                ViewBag.User = user;

                return View();
            }

            ViewBag.Message = "Вы не вошли в аккаунт";

            return View("Error");
        }

        [HttpPost]
        public IActionResult Add(Models.Task task)
        {
            return View("App");
        }
    }
}