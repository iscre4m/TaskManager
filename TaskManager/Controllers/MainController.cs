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

        private async Task<bool> IsUserSignedIn()
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.IsSignedIn == true);

            return user is not null;
        }

        private async Task<bool> IsTaskExists(string description)
        {
            Models.Task task = await _context.Tasks.FirstOrDefaultAsync(t => t.Description == description);

            return task is not null;
        }

        public async Task<IActionResult> App()
        {
            if (await IsUserSignedIn())
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.IsSignedIn == true);
                await _context.Entry(user).Collection(u => u.Tasks).LoadAsync();
                foreach (Models.Task task in user.Tasks)
                {
                    await _context.Entry(task).Collection("Subtasks").LoadAsync();
                }

                ViewBag.User = user;
                ViewBag.Tasks = user.Tasks;
                
                return View();
            }

            ViewBag.Message = "Вы не вошли в аккаунт";

            return View("Error");
        }

        public async Task<IActionResult> Error()
        {

            if (await IsUserSignedIn())
            {
                return View();
            }

            return Redirect("/Home/Index");
        }

        public async Task<IActionResult> Add()
        {
            if (await IsUserSignedIn())
            {
                return View();
            }

            ViewBag.Message = "Вы не вошли в аккаунт";

            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Add(Models.Task task)
        {
            if (await IsTaskExists(task.Description))
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
        public async Task<IActionResult> EditPage(int id)
        {
            if (await IsUserSignedIn())
            {
                Models.Task task = await _context.Tasks.FirstAsync(t => t.Id == id);
                await _context.Entry(task).Collection("Subtasks").LoadAsync();

                return View("Edit", task);
            }

            ViewBag.Message = "Вы не вошли в аккаунт";

            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Models.Task task)
        {
            if (await IsTaskExists(task.Description))
            {
                ViewBag.Message = "Задача с таким описанием уже существует";

                return View("Error");
            }

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

            return RedirectToAction("App", "Main");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            Models.Task queryTask = await _context.Tasks.FindAsync(id);
            await _context.Entry(queryTask).Collection("Subtasks").LoadAsync();

            foreach (Subtask subtask in queryTask.Subtasks)
            {
                _context.Subtasks.Remove(subtask);
            }

            _context.Tasks.Remove(queryTask);
            await _context.SaveChangesAsync();

            return RedirectToAction("App", "Main");
        }

        public async Task<IActionResult> Find(string description)
        {
            if (await IsUserSignedIn())
            {
                if (description is null)
                {
                    ViewBag.Message = "Введите описание задачи";

                    return View("Error");
                }
                
                User user = await _context.Users.FirstOrDefaultAsync(u => u.IsSignedIn == true);
                await _context.Entry(user).Collection(u => u.Tasks).LoadAsync();
                foreach (Models.Task task in user.Tasks)
                {
                    await _context.Entry(task).Collection("Subtasks").LoadAsync();
                }

                ViewBag.User = user;
                ViewBag.Tasks = user.Tasks.Where(t => t.Description.Contains(description));

                return View("App");
            }

            ViewBag.Message = "Вы не вошли в аккаунт";

            return View("Error");
        }

        #region Фильтры
        
        public async Task<IActionResult> FilterByDay()
        {
            if (await IsUserSignedIn())
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.IsSignedIn == true);
                await _context.Entry(user).Collection(u => u.Tasks).LoadAsync();
                foreach (Models.Task task in user.Tasks)
                {
                    await _context.Entry(task).Collection("Subtasks").LoadAsync();
                }

                ViewBag.User = user;
                ViewBag.Tasks = user.Tasks.Where(t => t.EndDate.Date == DateTime.Today);

                return View("App");
            }

            ViewBag.Message = "Вы не вошли в аккаунт";

            return View("Error");
        }

        public async Task<IActionResult> FilterByWeek()
        {
            if (await IsUserSignedIn())
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.IsSignedIn == true);
                await _context.Entry(user).Collection(u => u.Tasks).LoadAsync();
                foreach (Models.Task task in user.Tasks)
                {
                    await _context.Entry(task).Collection("Subtasks").LoadAsync();
                }

                ViewBag.User = user;
                ViewBag.Tasks = user.Tasks.Where(t => t.EndDate.Date <= DateTime.Today.AddDays(7));

                return View("App");
            }

            ViewBag.Message = "Вы не вошли в аккаунт";

            return View("Error");
        }

        public async Task<IActionResult> FilterByMonth()
        {
            if (await IsUserSignedIn())
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.IsSignedIn == true);
                await _context.Entry(user).Collection(u => u.Tasks).LoadAsync();
                foreach (Models.Task task in user.Tasks)
                {
                    await _context.Entry(task).Collection("Subtasks").LoadAsync();
                }

                ViewBag.User = user;
                ViewBag.Tasks = user.Tasks.Where(t => t.EndDate.Date <= DateTime.Today.AddMonths(1));

                return View("App");
            }

            ViewBag.Message = "Вы не вошли в аккаунт";

            return View("Error");
        }

        public async Task<IActionResult> FilterByYear()
        {
            if (await IsUserSignedIn())
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.IsSignedIn == true);
                await _context.Entry(user).Collection(u => u.Tasks).LoadAsync();
                foreach (Models.Task task in user.Tasks)
                {
                    await _context.Entry(task).Collection("Subtasks").LoadAsync();
                }

                ViewBag.User = user;
                ViewBag.Tasks = user.Tasks.Where(t => t.EndDate.Date <= DateTime.Today.AddYears(1));

                return View("App");
            }

            ViewBag.Message = "Вы не вошли в аккаунт";

            return View("Error");
        }

        #endregion

        #region Сортировки

        public async Task<IActionResult> SortByPriority()
        {
            if (await IsUserSignedIn())
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.IsSignedIn == true);
                await _context.Entry(user).Collection(u => u.Tasks).LoadAsync();
                foreach (Models.Task task in user.Tasks)
                {
                    await _context.Entry(task).Collection("Subtasks").LoadAsync();
                }

                ViewBag.User = user;
                ViewBag.Tasks = user.Tasks.OrderByDescending(t => t.Priority);

                return View("App");
            }

            ViewBag.Message = "Вы не вошли в аккаунт";

            return View("Error");
        }

        public async Task<IActionResult> SortByDate()
        {
            if (await IsUserSignedIn())
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.IsSignedIn == true);
                await _context.Entry(user).Collection(u => u.Tasks).LoadAsync();
                foreach (Models.Task task in user.Tasks)
                {
                    await _context.Entry(task).Collection("Subtasks").LoadAsync();
                }

                ViewBag.User = user;
                ViewBag.Tasks = user.Tasks.OrderBy(t => t.EndDate);

                return View("App");
            }

            ViewBag.Message = "Вы не вошли в аккаунт";

            return View("Error");
        }

        #endregion
    }
}