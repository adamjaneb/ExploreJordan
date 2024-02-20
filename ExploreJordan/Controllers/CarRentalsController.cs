using ExploreJordan.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExploreJordan.Controllers
{
    public class CarRentalsController : Controller
    {
        private readonly ICarsServices _carsService;
        public CarRentalsController(ICarsServices carsService)
        {
            _carsService = carsService;
        }

       
        public IActionResult Cars()
        {
            var cars = _carsService.GetAll();
            return View(cars);
        }
    }
}
