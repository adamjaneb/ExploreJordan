using ExploreJordan.Data;
using ExploreJordan.ViewModels;
using Microsoft.AspNetCore.Http;
using ExploreJordan.Models;
using SQLitePCL;
using ExploreJordan.Settings;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using System.Security.Claims;

namespace ExploreJordan.Services
{
	public class CarsServices: ICarsServices
	{
		private readonly ApplicationDbContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly string _imagesPath;

		public CarsServices(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_webHostEnvironment = webHostEnvironment;
			_imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
			_httpContextAccessor = httpContextAccessor;
		}


		public IEnumerable<Car> GetAll()
		{
			return _context.Cars.AsNoTracking().ToList();
		}
		public Car? GetById(int id)
		{
			return _context.Cars.AsNoTracking().SingleOrDefault(c => c.Id == id);
		}
		public async Task Create(CreateFormViewModel model)
		{
			var coverName = await SaveCover(model.Cover);

			var currentUserId = GetCurrentUserId();

			Car cars = new()
			{
				Name = model.Name,
				Description = model.Description,
				Price = model.Price,
				Cover = coverName,
				UserId = currentUserId,
				Address = model.Address

			};
			_context.Add(cars);
			_context.SaveChanges();

		}

		private string GetCurrentUserId()
		{
			var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			return userId;
		}


		public async Task<Car?> Edit(EditCarFormViewModel model)
        {
			var car = _context.Cars.Find(model.Id);

			if(car == null)
			{
				return null;
			}

			var hasNewCover = model.Cover is not null;
			var oldCover = car.Cover;
			car.Name = model.Name;
			car.Description = model.Description;	
			car.Price = model.Price;
			car.Address = model.Address;

			if (hasNewCover)
			{
				car.Cover = await SaveCover(model.Cover!);
			}
			var effectedRows = _context.SaveChanges();
			
			if(effectedRows > 0)
			{
				if (hasNewCover)
				{
					var cover = Path.Combine(_imagesPath, oldCover);
					File.Delete(cover);
				}
				return car;
			}
			else
			{
				var cover = Path.Combine(_imagesPath, car.Cover);
                File.Delete(cover);
                return null;

			}
		
        }
        public bool Delete(int id)
        {
			var isDeleted = false;
			var car = _context.Cars.Find(id);
			if(car == null)
			{
				return isDeleted;
			}
			_context.Cars.Remove(car);

			var effectedRows = _context.SaveChanges();
			if(effectedRows > 0)
			{
				isDeleted = true;
				var cover = Path.Combine(_imagesPath, car.Cover);
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

        
    }
	
}
