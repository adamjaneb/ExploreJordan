using ExploreJordan.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ExploreJordan.Data;
using ExploreJordan.Services;

namespace ExploreJordan.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly IActivitiesServices _activitiesService;

        public ActivitiesController(IActivitiesServices activitiesService)
        {
            _activitiesService = activitiesService;
        }

        public IActionResult activities()
        {
            var activities = _activitiesService.GetAll(); // You may need to implement this method in your service

            // Pass the list of activities to the view
            return View(activities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateActivitiesFormViewModel viewModel = new CreateActivitiesFormViewModel
            {

            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateActivitiesFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _activitiesService.Create(model);

            return RedirectToAction("Combined", "Combined");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var activity = _activitiesService.GetById(id);

            if (activity == null)
            {
                return NotFound();
            }

            EditActivitiesFormViewModel viewModel = new EditActivitiesFormViewModel
            {
                Id = id,
                Name = activity.Name,
                Description = activity.Description,
                Price = activity.Price,
                Address = activity.Address,
                City = activity.City
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditActivitiesFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var activity = await _activitiesService.Edit(model);

            if (activity == null)
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Activities");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _activitiesService.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
