using ExploreJordan.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ExploreJordan.Data;
using ExploreJordan.Services;
using Microsoft.AspNetCore.Http.HttpResults;


namespace ExploreJordan.Controllers
{
    public class CarsController : Controller
    {
		private readonly ICarsServices _carsServices;

        public CarsController(ICarsServices carsServices)
		{
			_carsServices = carsServices;
		}

        public IActionResult Index()
        {
            var cars = _carsServices.GetAll(); 

            return View(cars);
        }
        public IActionResult Details(int id)
        {
            var car = _carsServices.GetById(id);

            if (car == null)
            {
                return NotFound(); 
            }

            return View(car);
        }
        [HttpGet]
        public IActionResult Create() {

            CreateFormViewModel viewModel = new()
            {

            };

			return View(viewModel);
        }
		[HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateFormViewModel model)
		{
            if(!ModelState.IsValid) {
                return View(model);
            }

            await _carsServices.Create(model);

            return RedirectToAction("Combined", "Combined");
        }
        [HttpGet]
		public IActionResult Edit(int id)
		{
			var car = _carsServices.GetById(id);

			if (car == null)
			{
				return NotFound();
			}

			EditCarFormViewModel viewModel = new EditCarFormViewModel
			{
				Id = id,
				Name = car.Name,
				Description = car.Description,
				Price = car.Price,
                Address = car.Address
			};

			return View(viewModel);
		}
        [HttpPost]
        public async Task<IActionResult> Edit(EditCarFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
			var car = await _carsServices.Edit(model);
			if(car == null)
			{
				return BadRequest();
			}
			return RedirectToAction("Combined", "Combined");
		}
		[HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _carsServices.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
    
}

