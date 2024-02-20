using Microsoft.AspNetCore.Mvc;
using ExploreJordan.ViewModels;
using ExploreJordan.Services;

namespace ExploreJordan.Controllers
{
    public class CombinedController : Controller
    {
        private readonly ICarsServices _carsServices;
        private readonly IActivitiesServices _activitiesServices;
        private readonly IToursServices _toursServices;
        private readonly IAccommodationServices _accommodationServices;
        private readonly ISouvenirServices _souvenirsServices;

        public CombinedController(
            ICarsServices carsServices,
            IActivitiesServices activitiesServices,
            IToursServices toursServices,
            IAccommodationServices accommodationServices,
            ISouvenirServices souvenirsServices)
        {
            _carsServices = carsServices;
            _activitiesServices = activitiesServices;
            _toursServices = toursServices;
            _accommodationServices = accommodationServices;
            _souvenirsServices = souvenirsServices;
        }

        public IActionResult Combined()
        {
            var cars = _carsServices.GetAll();
            var activities = _activitiesServices.GetAll();
            var tours = _toursServices.GetAll();
            var accommodations = _accommodationServices.GetAll();
            var souvenirs = _souvenirsServices.GetAll();

            var viewModel = new CombinedViewModel
            {
                Cars = cars,
                Activities = activities,
                Tours = tours,
                Accommodations = accommodations,
                Souvenirs = souvenirs
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Search(string searchQuery)
        {
            var cars = _carsServices.GetAll();
            var activities = _activitiesServices.GetAll();
            var tours = _toursServices.GetAll();
            var accommodations = _accommodationServices.GetAll();
            var souvenirs = _souvenirsServices.GetAll();

            var viewModel = new CombinedViewModel
            {
                Cars = cars,
                Activities = activities,
                Tours = tours,
                Accommodations = accommodations,
                Souvenirs = souvenirs,
            };

            if (!String.IsNullOrEmpty(searchQuery))
            {
                // Filter Cars
                viewModel.Cars = viewModel.Cars.Where(car => car.Name.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) != -1).ToList();

                // Filter Activities
                viewModel.Activities = viewModel.Activities.Where(activity => activity.Name.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) != -1).ToList();

                // Filter Tours
                viewModel.Tours = viewModel.Tours.Where(tour => tour.Name.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) != -1).ToList();

                // Filter Accommodations
                viewModel.Accommodations = viewModel.Accommodations.Where(accommodation => accommodation.Name.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) != -1).ToList();

                // Filter Souvenirs
                viewModel.Souvenirs = viewModel.Souvenirs.Where(souvenir => souvenir.Name.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) != -1).ToList();
            }
            else
            {
                ViewBag.NoSearchesFound = "No searches found.";
            }

            return View(viewModel);
        }
    }
}
