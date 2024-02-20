using ExploreJordan.Data;
using ExploreJordan.ViewModels;
using Microsoft.AspNetCore.Http;
using ExploreJordan.Models;
using SQLitePCL;
using ExploreJordan.Settings;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using System.Security.Claims;
using SendGrid.Helpers.Mail;

namespace ExploreJordan.Services

{
    public class ActivitiesServices : IActivitiesServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _imagesPath;

        public ActivitiesServices(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
            _httpContextAccessor = httpContextAccessor;
        }
        public IEnumerable<Activities> GetAll()
        {
            return _context.Activities.AsNoTracking().ToList();
        }

        public Activities? GetById(int id)
        {
            return _context.Activities.AsNoTracking().SingleOrDefault(c => c.Id == id);
        }
        public async Task Create(CreateActivitiesFormViewModel model)
        {
            var coverName = await SaveCover(model.Cover);

            // Access the current user's ID from HttpContext
            var currentUserId = GetCurrentUserId();

            Activities activities = new()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Cover = coverName,
                UserId = currentUserId,
                Address = model.Address,
                City = model.City

            };
            _context.Add(activities);
            _context.SaveChanges();
        }
        private string GetCurrentUserId()
        {
            // Access the current user's ID from HttpContext
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return userId;
        }

        public bool Delete(int id)
        {
            var isDeleted = false;
            var activities = _context.Activities.Find(id);
            if (activities == null)
            {
                return isDeleted;
            }
            _context.Activities.Remove(activities);

            var effectedRows = _context.SaveChanges();
            if (effectedRows > 0)
            {
                isDeleted = true;
                var cover = Path.Combine(_imagesPath, activities.Cover);
                File.Delete(cover);
            }
            return isDeleted;
        }
        private async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

            var path = Path.Combine(_imagesPath, coverName);

            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);

            return coverName;
        }
        public async Task<Activities?> Edit(EditActivitiesFormViewModel model)
        {
            var activities = _context.Activities.Find(model.Id);

            if (activities == null)
            {
                return null;
            }

            var hasNewCover = model.Cover is not null;
            var oldCover = activities.Cover;
            activities.Name = model.Name;
            activities.Description = model.Description;
            activities.Price = model.Price;
            activities.City = model.City;


            if (hasNewCover)
            {
                activities.Cover = await SaveCover(model.Cover!);
            }
            var effectedRows = _context.SaveChanges();

            if (effectedRows > 0)
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(_imagesPath, oldCover);
                    File.Delete(cover);
                }
                return activities;
            }
            else
            {
                var cover = Path.Combine(_imagesPath, activities.Cover);
                File.Delete(cover);
                return null;

            }
        }


    }
}
