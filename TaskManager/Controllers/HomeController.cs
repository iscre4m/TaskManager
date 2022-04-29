using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskManagerContext _context;

        public HomeController(TaskManagerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.IsSignedIn == true);

            if (user is not null)
            {
                return RedirectToAction("App", "Main");
            }

            return View();
        }
    }
}