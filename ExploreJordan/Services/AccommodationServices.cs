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
    public class AccommodationServices : IAccommodationServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _imagesPath;

        public AccommodationServices(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
            _httpContextAccessor = httpContextAccessor;
        }
        public IEnumerable<Accommodation> GetAll()
        {
            return _context.Accommodation.AsNoTracking().ToList();
        }

        public Accommodation? GetById(int id)
        {
            return _context.Accommodation.AsNoTracking().SingleOrDefault(c => c.Id == id);
        }
        public async Task Create(CreateAccommodationFormViewModel model)
        {
            var coverName = await SaveCover(model.Cover);

            // Access the current user's ID from HttpContext
            var currentUserId = GetCurrentUserId();

            Accommodation accommodation = new()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Cover = coverName,
                UserId = currentUserId,
                Address = model.Address,
                City = model.City
            };
            _context.Add(accommodation);
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
            var accommodation = _context.Accommodation.Find(id);
            if (accommodation == null)
            {
                return isDeleted;
            }
            _context.Accommodation.Remove(accommodation);

            var effectedRows = _context.SaveChanges();
            if (effectedRows > 0)
            {
                isDeleted = true;
                var cover = Path.Combine(_imagesPath, accommodation.Cover);
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
        public async Task<Accommodation?> Edit(EditAccommodationFormViewModel model)
        {
            var accommodation = _context.Accommodation.Find(model.Id);

            if (accommodation == null)
            {
                return null;
            }

            var hasNewCover = model.Cover is not null;
            var oldCover = accommodation.Cover;
            accommodation.Name = model.Name;
            accommodation.Description = model.Description;
            accommodation.Price = model.Price;
            accommodation.City = model.City;
           

            if (hasNewCover)
            {
                accommodation.Cover = await SaveCover(model.Cover!);
            }
            var effectedRows = _context.SaveChanges();

            if (effectedRows > 0)
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(_imagesPath, oldCover);
                    File.Delete(cover);
                }
                return accommodation;
            }
            else
            {
                var cover = Path.Combine(_imagesPath, accommodation.Cover);
                File.Delete(cover);
                return null;

            }
        }


    }
}
