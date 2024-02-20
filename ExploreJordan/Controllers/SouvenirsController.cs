using ExploreJordan.Services;
using ExploreJordan.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExploreJordan.Controllers
{
    public class SouvenirsController : Controller
    {
        private readonly ISouvenirServices _souvenirService;

        public SouvenirsController(ISouvenirServices souvenirService)
        {
            _souvenirService = souvenirService;
        }

        public IActionResult souvenirs()
        {
            var souvenirs = _souvenirService.GetAll();
            return View(souvenirs);
        }

        public IActionResult Details(int id)
        {
            var souvenirs = _souvenirService.GetById(id);

            if (souvenirs == null)
            {
                return NotFound(); // Or handle the case where the car is not found
            }

            return View(souvenirs);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateSouvenirFromViewModel viewModel = new CreateSouvenirFromViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSouvenirFromViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _souvenirService.Create(model);

            return RedirectToAction("Combined", "Combined");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var souvenir = _souvenirService.GetById(id);

            if (souvenir == null)
            {
                return NotFound();
            }

            EditSouvenirFormViewModel viewModel = new EditSouvenirFormViewModel
            {
                Id = id,
                Name = souvenir.Name,
                Description = souvenir.Description,
                Price = souvenir.Price,
                Quantity = souvenir.Quantity
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditSouvenirFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var souvenir = await _souvenirService.Edit(model);

            if (souvenir == null)
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Cars"); // Assuming there is an Index action in the Cars controller
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _souvenirService.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int id)
        {
            var souvenir = _souvenirService.GetById(id);

            if (souvenir != null && souvenir.Quantity > 0)
            {
                souvenir.Quantity -= 1;
                _souvenirService.Edit(new EditSouvenirFormViewModel
                {
                    Id = souvenir.Id,
                    Name = souvenir.Name,
                    Description = souvenir.Description,
                    Price = souvenir.Price,
                    Quantity = souvenir.Quantity
                    // Include other properties if needed
                });

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

    }
}
