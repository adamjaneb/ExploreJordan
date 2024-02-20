using ExploreJordan.Services;
using ExploreJordan.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExploreJordan.Controllers
{
    public class PopularDestinationsController : Controller
    {
        private readonly IActivitiesServices _activitiesServices;
        private readonly IToursServices _toursServices;
        private readonly IAccommodationServices _accommodationServices;

        public PopularDestinationsController(
            IActivitiesServices activitiesServices,
            IToursServices toursServices,
            IAccommodationServices accommodationServices)
        {
            _activitiesServices = activitiesServices;
            _toursServices = toursServices;
            _accommodationServices = accommodationServices;
        }
        public IActionResult Amman()
        {
            var activities = _activitiesServices.GetAll();
            var tours = _toursServices.GetAll();
            var accommodations = _accommodationServices.GetAll();

            var viewModel = new CombinedViewModel
            {
                Activities = activities,
                Tours = tours,
                Accommodations = accommodations,
            };

            return View(viewModel);
        }
        public IActionResult DeadSea()
        {
            var activities = _activitiesServices.GetAll();
            var tours = _toursServices.GetAll();
            var accommodations = _accommodationServices.GetAll();

            var viewModel = new CombinedViewModel
            {
                Activities = activities,
                Tours = tours,
                Accommodations = accommodations,
            };

            return View(viewModel);
        }
        public IActionResult Ajloun()
        {
            var activities = _activitiesServices.GetAll();
            var tours = _toursServices.GetAll();
            var accommodations = _accommodationServices.GetAll();

            var viewModel = new CombinedViewModel
            {
                Activities = activities,
                Tours = tours,
                Accommodations = accommodations,
            };

            return View(viewModel);
        }
        public IActionResult Madaba()
        {
            var activities = _activitiesServices.GetAll();
            var tours = _toursServices.GetAll();
            var accommodations = _accommodationServices.GetAll();

            var viewModel = new CombinedViewModel
            {
                Activities = activities,
                Tours = tours,
                Accommodations = accommodations,
            };

            return View(viewModel);
        }
        public IActionResult Aqaba()
        {
            var activities = _activitiesServices.GetAll();
            var tours = _toursServices.GetAll();
            var accommodations = _accommodationServices.GetAll();

            var viewModel = new CombinedViewModel
            {
                Activities = activities,
                Tours = tours,
                Accommodations = accommodations,
            };

            return View(viewModel);
        }
        public IActionResult Jerash()
        {
            var activities = _activitiesServices.GetAll();
            var tours = _toursServices.GetAll();
            var accommodations = _accommodationServices.GetAll();

            var viewModel = new CombinedViewModel
            {
                Activities = activities,
                Tours = tours,
                Accommodations = accommodations,
            };

            return View(viewModel);
        }
        public IActionResult Rum()
        {
            var activities = _activitiesServices.GetAll();
            var tours = _toursServices.GetAll();
            var accommodations = _accommodationServices.GetAll();

            var viewModel = new CombinedViewModel
            {
                Activities = activities,
                Tours = tours,
                Accommodations = accommodations,
            };

            return View(viewModel);
        }
        public IActionResult Petra()
        {
            var activities = _activitiesServices.GetAll();
            var tours = _toursServices.GetAll();
            var accommodations = _accommodationServices.GetAll();

            var viewModel = new CombinedViewModel
            {
                Activities = activities,
                Tours = tours,
                Accommodations = accommodations,
            };

            return View(viewModel);
        }

    }
}
