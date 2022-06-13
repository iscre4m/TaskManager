using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return Content(User.Identity.Name);
        }

        //public async Task<IActionResult> Error()
        //{
        //    ViewBag.Message = await System.IO.File.ReadAllTextAsync("Data/error.txt");

        //    return View();
        //}
    }
}