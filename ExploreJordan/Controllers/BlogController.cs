using Microsoft.AspNetCore.Mvc;

namespace GradProject.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Blog()
        {
            return View("Blog");
        }

        public IActionResult Blog1()
        {
            return View("Blog1");
        }

        public IActionResult Blog2()
        {
            return View("Blog2");
        }
        public IActionResult Blog3()
        {
            return View("Blog3");
        }

        public IActionResult Blog4()
        {
            return View("Blog4");
        }

        public IActionResult Blog5()
        {
            return View("Blog5");
        }

        public IActionResult Blog6()
        {
            return View("Blog6");
        }
    }
}
