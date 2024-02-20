using ExploreJordan.Attributes;
using ExploreJordan.Settings;

namespace ExploreJordan.ViewModels
{
    public class EditCarFormViewModel : CarFormViewModel
    {
        public int Id { get; set; }

        [AllowedExtensions(FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxFileSizeInBytes)]

		public string Address { get; set; }

		public IFormFile? Cover { get; set; } = default;
    }
}
