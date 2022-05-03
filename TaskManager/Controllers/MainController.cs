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
                await _context.Entry(user).Collection(u => u.Tasks).LoadAsync();
                ViewBag.User = user;

                return View();
            }

            ViewBag.Message = "Вы не вошли в аккаунт";

            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Add(Models.Task task)
        {
            await _context.Tasks.AddAsync(task);
            (await _context.Users.FirstAsync(u => u.IsSignedIn == true)).Tasks.Add(task);
            await _context.SaveChangesAsync();

            return RedirectToAction("App", "Main");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Models.Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

            return RedirectToAction("App", "Main");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Models.Task task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return RedirectToAction("App", "Main");
        }
    }
}