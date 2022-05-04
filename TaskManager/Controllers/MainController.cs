using System;
using System.Linq;
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
            if (ViewBag.User is not null)
            {
                return View();
            }

            User user = await _context.Users.FirstOrDefaultAsync(u => u.IsSignedIn == true);

            if (user is not null)
            {
                await _context.Entry(user).Collection(u => u.Tasks).LoadAsync();
                foreach(Models.Task task in user.Tasks)
                {
                    await _context.Entry(task).Collection("Subtasks").LoadAsync();
                }
                ViewBag.User = user;

                return View();
            }

            ViewBag.Message = "Вы не вошли в аккаунт";

            return View("Error");
        }

        public async Task<IActionResult> Error()
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.IsSignedIn == true);

            if (user is not null)
            {
                return View();
            }

            return Redirect("/Home/Index");
        }

        [HttpPost]
        public async Task<IActionResult> Add(Models.Task task)
        {
            Models.Task queryTask = await _context.Tasks.FirstOrDefaultAsync(t => t.Description == task.Description);
            
            if (queryTask is not null)
            {
                ViewBag.Message = "Задача с таким описанием уже существует";

                return View("Error");
            }

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
            Models.Task queryTask = await _context.Tasks.FindAsync(task.Id);
            await _context.Entry(queryTask).Collection("Subtasks").LoadAsync();
            
            foreach (Subtask subtask in queryTask.Subtasks)
            {
                _context.Subtasks.Remove(subtask);
            }
            _context.Tasks.Remove(queryTask);
            await _context.SaveChangesAsync();

            return RedirectToAction("App", "Main");
        }
    }
}