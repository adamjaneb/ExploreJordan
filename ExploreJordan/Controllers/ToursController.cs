using ExploreJordan.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ExploreJordan.Data;
using ExploreJordan.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace ExploreJordan.Controllers
{
    public class ToursController : Controller
    {
        private readonly IToursServices _toursService;

        public ToursController(IToursServices toursService)
        {
            _toursService = toursService;
        }

        public IActionResult tours()
        {
            var tours = _toursService.GetAll(); 

            return View(tours);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateToursFormViewModel viewModel = new CreateToursFormViewModel
            {

            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateToursFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _toursService.Create(model);

            return RedirectToAction("Combined", "Combined");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var tour = _toursService.GetById(id);

            if (tour == null)
            {
                return NotFound();
            }

            EditToursFormViewModel viewModel = new EditToursFormViewModel
            {
                Id = id,
                Name = tour.Name,
                Description = tour.Description,
                Price = tour.Price,
                Address = tour.Address,
                City = tour.City
               
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditToursFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var tour = await _toursService.Edit(model);

            if (tour == null)
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Tours");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _toursService.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
