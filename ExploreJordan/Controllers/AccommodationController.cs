using ExploreJordan.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ExploreJordan.Data;
using ExploreJordan.Services;
using Microsoft.AspNetCore.Http.HttpResults;


namespace ExploreJordan.Controllers
{
    public class AccommodationController : Controller
    {
        private readonly IAccommodationServices _accommodationService;

        public AccommodationController(IAccommodationServices accommodationService)
        {
            _accommodationService = accommodationService;
        }
        public IActionResult accommodation()
        {
            var accommodations = _accommodationService.GetAll();

            return View(accommodations);
        }

        public IActionResult Details(int id)
        {
            var accommodation = _accommodationService.GetById(id);

            if (accommodation == null)
            {
                return NotFound(); 
            }

            return View(accommodation);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateAccommodationFormViewModel viewModel = new()
            {

            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAccommodationFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _accommodationService.Create(model);

            return RedirectToAction("Combined", "Combined");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var accommodation = _accommodationService.GetById(id);

            if (accommodation == null)
            {
                return NotFound();
            }

            EditAccommodationFormViewModel viewModel = new EditAccommodationFormViewModel
            {
                Id = id,
                Name = accommodation.Name,
                Description = accommodation.Description,
                Price = accommodation.Price,
                Address = accommodation.Address,
                City = accommodation.City
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditAccommodationFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var accommodation = await _accommodationService.Edit(model);

            if (accommodation == null)
            {
                return BadRequest();
            }

            return RedirectToAction("Combined", "Combined");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _accommodationService.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
