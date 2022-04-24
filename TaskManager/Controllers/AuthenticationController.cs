using Microsoft.AspNetCore.Mvc;
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

        public IActionResult SignUp() => View();

        public IActionResult SignIn() => View();
    }
}