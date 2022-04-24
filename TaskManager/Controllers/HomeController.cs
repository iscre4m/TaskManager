﻿using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index() => View();
    }
}