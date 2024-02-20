using ExploreJordan.Attributes;
using ExploreJordan.Settings;

namespace ExploreJordan.ViewModels
{
	public class EditAccommodationFormViewModel
	{
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Double Price { get; set; }
        [AllowedExtensions(FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public string Address { get; set; }
        public string City { get; set; }
        public IFormFile? Cover { get; set; } = default;
        public int Id { get; internal set; }
    }
}
