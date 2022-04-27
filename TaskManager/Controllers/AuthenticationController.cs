using Microsoft.AspNetCore.Mvc;
using TaskManager.Data;

namespace TaskManager.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly TaskManagerContext _context;

        public AuthenticationController(TaskManagerContext context)
        {
            _context = context;
        }

        public IActionResult SignUp()
        {
            return Redirect("/Home/Index");
        }

        public IActionResult SignIn()
        {
            return Redirect("/Home/Index");
        }
    }
}