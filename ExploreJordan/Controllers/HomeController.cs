using ExploreJordan.Models;
using GradProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GradProject.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Homepage()
		{
			return View("homepage");
		}
		public IActionResult Aboutus()
		{
			return View("aboutus");
		}

		public IActionResult Contactus()
		{
			return View("contactus");
		}



		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
