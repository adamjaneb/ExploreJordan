using ExploreJordan.Attributes;
using ExploreJordan.Settings;

namespace ExploreJordan.ViewModels
{
	public class CreateAccommodationFormViewModel
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public Double Price { get; set; }
		[AllowedExtensions(FileSettings.AllowedExtensions),
			MaxFileSize(FileSettings.MaxFileSizeInBytes)]
		public string UserId { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public IFormFile Cover { get; set; } = default;
	}
}
