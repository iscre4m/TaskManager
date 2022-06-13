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

            return View(user);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Models.Task task)
        {
            if(await _context.Tasks.FirstOrDefaultAsync(
                t => t.Description == task.Description) is not null)
            {
                await _context.Tasks.AddAsync(task);
                (await _context.Users.FirstAsync(u => u.Username == User.Identity.Name)).Tasks.Add(task);
                await _context.SaveChangesAsync();

                return RedirectToAction("App", "Main");
            }

            ModelState.AddModelError("", "Задача с таким описанием уже существует");

            return View(task);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Models.Task task = await _context.Tasks.FindAsync(id);

            await _context.Entry(task).Collection("Subtasks").LoadAsync();
            await _context.Entry(task).Collection("Hashtags").LoadAsync();
            await _context.Entry(task).Collection("Attachments").LoadAsync();

            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Models.Task task)
        {
            if(_context.Tasks.Where(t => t.Description == task.Description).Count() < 2)
            {
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();

                return RedirectToAction("App", "Main");
            }

            ModelState.AddModelError("", "Задача с таким описанием уже существует");

            return View(task);
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
            await _context.SaveChangesAsync();

            return RedirectToAction("App", "Main");
        }

        //public async Task<IActionResult> Find(string description)
        //{
        //    if (await IsUserSignedIn())
        //    {
        //        if (description is null)
        //        {
        //            await System.IO.File.WriteAllTextAsync("Data/error.txt", "Вы не ввели описание задачи");

        //            return RedirectToAction("Error", "Home");
        //        }

        //        User user = await _context.Users.FirstOrDefaultAsync(u => u.IsSignedIn == true);
        //        await _context.Entry(user).Collection(u => u.Tasks).LoadAsync();
        //        foreach (Models.Task task in user.Tasks)
        //        {
        //            await _context.Entry(task).Collection("Subtasks").LoadAsync();
        //        }

        //        ViewBag.Tasks = user.Tasks.Where(t => t.Description.Contains(description));

        //        return View("App");
        //    }

        //    await System.IO.File.WriteAllTextAsync("Data/error.txt", "Вы не вошли в аккаунт");

        //    return RedirectToAction("Error", "Home");
        //}

        //#region Фильтры

        //public async Task<IActionResult> FilterByDay()
        //{
        //    if (await IsUserSignedIn())
        //    {
        //        User user = await _context.Users.FirstOrDefaultAsync(u => u.IsSignedIn == true);
        //        await _context.Entry(user).Collection(u => u.Tasks).LoadAsync();
        //        foreach (Models.Task task in user.Tasks)
        //        {
        //            await _context.Entry(task).Collection("Subtasks").LoadAsync();
        //        }

        //        ViewBag.Tasks = user.Tasks.Where(t => t.EndDate.Date == DateTime.Today);

        //        return View("App");
        //    }

        //    await System.IO.File.WriteAllTextAsync("Data/error.txt", "Вы не вошли в аккаунт");

        //    return RedirectToAction("Error", "Home");
        //}

        //public async Task<IActionResult> FilterByWeek()
        //{
        //    if (await IsUserSignedIn())
        //    {
        //        User user = await _context.Users.FirstOrDefaultAsync(u => u.IsSignedIn == true);
        //        await _context.Entry(user).Collection(u => u.Tasks).LoadAsync();
        //        foreach (Models.Task task in user.Tasks)
        //        {
        //            await _context.Entry(task).Collection("Subtasks").LoadAsync();
        //        }

        //        ViewBag.Tasks = user.Tasks.Where(t => t.EndDate.Date <= DateTime.Today.AddDays(7));

        //        return View("App");
        //    }

        //    await System.IO.File.WriteAllTextAsync("Data/error.txt", "Вы не вошли в аккаунт");

        //    return RedirectToAction("Error", "Home");
        //}

        //public async Task<IActionResult> FilterByMonth()
        //{
        //    if (await IsUserSignedIn())
        //    {
        //        User user = await _context.Users.FirstOrDefaultAsync(u => u.IsSignedIn == true);
        //        await _context.Entry(user).Collection(u => u.Tasks).LoadAsync();
        //        foreach (Models.Task task in user.Tasks)
        //        {
        //            await _context.Entry(task).Collection("Subtasks").LoadAsync();
        //        }

        //        ViewBag.Tasks = user.Tasks.Where(t => t.EndDate.Date <= DateTime.Today.AddMonths(1));

        //        return View("App");
        //    }

        //    await System.IO.File.WriteAllTextAsync("Data/error.txt", "Вы не вошли в аккаунт");

        //    return RedirectToAction("Error", "Home");
        //}

        //public async Task<IActionResult> FilterByYear()
        //{
        //    if (await IsUserSignedIn())
        //    {
        //        User user = await _context.Users.FirstOrDefaultAsync(u => u.IsSignedIn == true);
        //        await _context.Entry(user).Collection(u => u.Tasks).LoadAsync();
        //        foreach (Models.Task task in user.Tasks)
        //        {
        //            await _context.Entry(task).Collection("Subtasks").LoadAsync();
        //        }

        //        ViewBag.Tasks = user.Tasks.Where(t => t.EndDate.Date <= DateTime.Today.AddYears(1));

        //        return View("App");
        //    }

        //    await System.IO.File.WriteAllTextAsync("Data/error.txt", "Вы не вошли в аккаунт");

        //    return RedirectToAction("Error", "Home");
        //}

        //#endregion

        //#region Сортировки

        //public async Task<IActionResult> SortByPriority()
        //{
        //    if (await IsUserSignedIn())
        //    {
        //        User user = await _context.Users.FirstOrDefaultAsync(u => u.IsSignedIn == true);
        //        await _context.Entry(user).Collection(u => u.Tasks).LoadAsync();
        //        foreach (Models.Task task in user.Tasks)
        //        {
        //            await _context.Entry(task).Collection("Subtasks").LoadAsync();
        //        }

        //        ViewBag.Tasks = user.Tasks.OrderByDescending(t => t.Priority);

        //        return View("App");
        //    }

        //    await System.IO.File.WriteAllTextAsync("Data/error.txt", "Вы не вошли в аккаунт");

        //    return RedirectToAction("Error", "Home");
        //}

        //public async Task<IActionResult> SortByDate()
        //{
        //    if (await IsUserSignedIn())
        //    {
        //        User user = await _context.Users.FirstOrDefaultAsync(u => u.IsSignedIn == true);
        //        await _context.Entry(user).Collection(u => u.Tasks).LoadAsync();
        //        foreach (Models.Task task in user.Tasks)
        //        {
        //            await _context.Entry(task).Collection("Subtasks").LoadAsync();
        //        }

        //        ViewBag.Tasks = user.Tasks.OrderBy(t => t.EndDate);

        //        return View("App");
        //    }

        //    await System.IO.File.WriteAllTextAsync("Data/error.txt", "Вы не вошли в аккаунт");

        //    return RedirectToAction("Error", "Home");
        //}

        //#endregion
    }
}