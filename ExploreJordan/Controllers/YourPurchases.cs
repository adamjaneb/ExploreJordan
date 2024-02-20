using Microsoft.AspNetCore.Mvc;

namespace ExploreJordan.Controllers
{
    public class YourPurchases : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
