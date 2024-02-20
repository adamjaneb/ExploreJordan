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
    public class ToursServices : IToursServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _imagesPath;

        public ToursServices(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
            _httpContextAccessor = httpContextAccessor;
        }
        public IEnumerable<Tours> GetAll()
        {
            return _context.Tours.AsNoTracking().ToList();
        }

        public Tours? GetById(int id)
        {
            return _context.Tours.AsNoTracking().SingleOrDefault(c => c.Id == id);
        }
        public async Task Create(CreateToursFormViewModel model)
        {
            var coverName = await SaveCover(model.Cover);

            // Access the current user's ID from HttpContext
            var currentUserId = GetCurrentUserId();

            Tours tours = new()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Cover = coverName,
                UserId = currentUserId,
                Address = model.Address,
                City = model.City
            };
            _context.Add(tours);
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
            var tours = _context.Tours.Find(id);
            if (tours == null)
            {
                return isDeleted;
            }
            _context.Tours.Remove(tours);

            var effectedRows = _context.SaveChanges();
            if (effectedRows > 0)
            {
                isDeleted = true;
                var cover = Path.Combine(_imagesPath, tours.Cover);
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
        public async Task<Tours?> Edit(EditToursFormViewModel model)
        {
            var tours = _context.Tours.Find(model.Id);

            if (tours == null)
            {
                return null;
            }

            var hasNewCover = model.Cover is not null;
            var oldCover = tours.Cover;
            tours.Name = model.Name;
            tours.Description = model.Description;
            tours.Price = model.Price;
            tours.City = model.City;


            if (hasNewCover)
            {
                tours.Cover = await SaveCover(model.Cover!);
            }
            var effectedRows = _context.SaveChanges();

            if (effectedRows > 0)
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(_imagesPath, oldCover);
                    File.Delete(cover);
                }
                return tours;
            }
            else
            {
                var cover = Path.Combine(_imagesPath, tours.Cover);
                File.Delete(cover);
                return null;

            }
        }

    }
}
