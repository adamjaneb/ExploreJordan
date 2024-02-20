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
    public class SouvenirServices : ISouvenirServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _imagesPath;

        public SouvenirServices(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
            _httpContextAccessor = httpContextAccessor;
        }
        public IEnumerable<Souvenirs> GetAll()
        {
            return _context.Souvenirs.AsNoTracking().ToList();
        }

        public Souvenirs? GetById(int id)
        {
            return _context.Souvenirs.AsNoTracking().SingleOrDefault(c => c.Id == id);
        }
        public async Task Create(CreateSouvenirFromViewModel model)
        {
            var coverName = await SaveCover(model.Cover);

            // Access the current user's ID from HttpContext
            var currentUserId = GetCurrentUserId();

            Souvenirs souvenir = new()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Cover = coverName,
                UserId = currentUserId,
                Quantity = model.Quantity,
            };
            _context.Add(souvenir);
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
            var souvenir = _context.Souvenirs.Find(id);
            if (souvenir == null)
            {
                return isDeleted;
            }
            _context.Souvenirs.Remove(souvenir);

            var effectedRows = _context.SaveChanges();
            if (effectedRows > 0)
            {
                isDeleted = true;
                var cover = Path.Combine(_imagesPath, souvenir.Cover);
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
        public async Task<Souvenirs?> Edit(EditSouvenirFormViewModel model)
        {
            var souvenir = _context.Souvenirs.Find(model.Id);

            if (souvenir == null)
            {
                return null;
            }

            var hasNewCover = model.Cover is not null;
            var oldCover = souvenir.Cover;
            souvenir.Name = model.Name;
            souvenir.Description = model.Description;
            souvenir.Price = model.Price;
            souvenir.Quantity = model.Quantity;

            if (hasNewCover)
            {
                souvenir.Cover = await SaveCover(model.Cover!);
            }
            var effectedRows = _context.SaveChanges();

            if (effectedRows > 0)
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(_imagesPath, oldCover);
                    File.Delete(cover);
                }
                return souvenir;
            }
            else
            {
                var cover = Path.Combine(_imagesPath, souvenir.Cover);
                File.Delete(cover);
                return null;

            }
        }

       
    }
}
