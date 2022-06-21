using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

        [Authorize]
        public async Task<IActionResult> App()
        {
            User user = await _context.Users.FirstAsync(u => u.Username == User.Identity.Name);

            await _context.Entry(user).Collection("Tasks").LoadAsync();
            foreach(var task in user.Tasks)
            {
                await _context.Entry(task).Collection("Subtasks").LoadAsync();
            }

            return View(user.Tasks);
        }

        #region CRUD

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Models.Task task)
        {
            if(await _context.Tasks.AnyAsync(t => t.Description == task.Description))
            {
                ModelState.AddModelError("", "Задача с таким описанием уже существует");

                return View(task);
            }

            await _context.Tasks.AddAsync(task);
            (await _context.Users.FirstAsync(u => u.Username == User.Identity.Name)).Tasks.Add(task);
            await _context.SaveChangesAsync();

            return RedirectToAction("App", "Main");
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("App", "Main");
            }

            Models.Task task = await _context.Tasks.FindAsync(id);

            await _context.Entry(task).Collection("Subtasks").LoadAsync();
            await _context.Entry(task).Collection("Hashtags").LoadAsync();
            await _context.Entry(task).Collection("Attachments").LoadAsync();

            return View(task);
        }

        private async System.Threading.Tasks.Task RemoveTaskFromDatabase(int id)
        {
            Models.Task queryTask = await _context.Tasks.FindAsync(id);

            await _context.Entry(queryTask).Collection("Subtasks").LoadAsync();
            foreach (Subtask subtask in queryTask.Subtasks)
            {
                _context.Subtasks.Remove(subtask);
            }

            await _context.Entry(queryTask).Collection("Hashtags").LoadAsync();
            foreach (Hashtag hashtag in queryTask.Hashtags)
            {
                _context.Hashtags.Remove(hashtag);
            }

            await _context.Entry(queryTask).Collection("Attachments").LoadAsync();
            foreach (Attachment attachment in queryTask.Attachments)
            {
                _context.Attachments.Remove(attachment);
            }

            _context.Tasks.Remove(queryTask);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Models.Task task)
        {
            if(await _context.Tasks.AnyAsync(t => t.Description == task.Description && t.Id != task.Id))
            {
                ModelState.AddModelError("", "Задача с таким описанием уже существует");

                return View(task);
            }

            await RemoveTaskFromDatabase(task.Id);
            await _context.Tasks.AddAsync(task);
            (await _context.Users.FirstAsync(u => u.Username == User.Identity.Name)).Tasks.Add(task);
            await _context.SaveChangesAsync();

            return RedirectToAction("App", "Main");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("App", "Main");
            }

            await RemoveTaskFromDatabase(id);
            await _context.SaveChangesAsync();

            return RedirectToAction("App", "Main");
        }

        [Authorize]
        public async Task<IActionResult> Find(string description)
        {
            User user = await _context.Users.FirstAsync(u => u.Username == User.Identity.Name);

            await _context.Entry(user).Collection("Tasks").LoadAsync();
            foreach (var task in user.Tasks)
            {
                await _context.Entry(task).Collection("Subtasks").LoadAsync();
            }

            if(description is null)
            {
                description = string.Empty;
            }

            return View("App", user.Tasks.Where(t => t.Description.Contains(description)).ToList());
        }

        #endregion

        #region Фильтры

        public async Task<IActionResult> FilterByDay()
        {
            User user = await _context.Users.FirstAsync(u => u.Username == User.Identity.Name);

            await _context.Entry(user).Collection("Tasks").LoadAsync();
            foreach (var task in user.Tasks)
            {
                await _context.Entry(task).Collection("Subtasks").LoadAsync();
            }

            return View("App", user.Tasks.Where(t => t.EndDate.Date == DateTime.Today).ToList());
        }

        public async Task<IActionResult> FilterByWeek()
        {
            User user = await _context.Users.FirstAsync(u => u.Username == User.Identity.Name);

            await _context.Entry(user).Collection("Tasks").LoadAsync();
            foreach (var task in user.Tasks)
            {
                await _context.Entry(task).Collection("Subtasks").LoadAsync();
            }

            return View("App", user.Tasks.Where(t => t.EndDate.Date <= DateTime.Today.AddDays(7)).ToList());
        }

        public async Task<IActionResult> FilterByMonth()
        {
            User user = await _context.Users.FirstAsync(u => u.Username == User.Identity.Name);

            await _context.Entry(user).Collection("Tasks").LoadAsync();
            foreach (var task in user.Tasks)
            {
                await _context.Entry(task).Collection("Subtasks").LoadAsync();
            }

            return View("App", user.Tasks.Where(t => t.EndDate.Date <= DateTime.Today.AddMonths(1)).ToList());
        }

        public async Task<IActionResult> FilterByYear()
        {
            User user = await _context.Users.FirstAsync(u => u.Username == User.Identity.Name);

            await _context.Entry(user).Collection("Tasks").LoadAsync();
            foreach (var task in user.Tasks)
            {
                await _context.Entry(task).Collection("Subtasks").LoadAsync();
            }

            return View("App", user.Tasks.Where(t => t.EndDate.Date <= DateTime.Today.AddYears(1)).ToList());
        }

        #endregion

        #region Сортировки

        [Authorize]
        public async Task<IActionResult> SortByPriority()
        {
            User user = await _context.Users.FirstAsync(u => u.Username == User.Identity.Name);

            await _context.Entry(user).Collection("Tasks").LoadAsync();
            foreach (var task in user.Tasks)
            {
                await _context.Entry(task).Collection("Subtasks").LoadAsync();
            }

            return View("App", user.Tasks.OrderByDescending(t => t.Priority).ToList());
        }

        [Authorize]
        public async Task<IActionResult> SortByDate()
        {
            User user = await _context.Users.FirstAsync(u => u.Username == User.Identity.Name);

            await _context.Entry(user).Collection("Tasks").LoadAsync();
            foreach (var task in user.Tasks)
            {
                await _context.Entry(task).Collection("Subtasks").LoadAsync();
            }

            return View("App", user.Tasks.OrderBy(t => t.EndDate).ToList());
        }

        #endregion
    }
}