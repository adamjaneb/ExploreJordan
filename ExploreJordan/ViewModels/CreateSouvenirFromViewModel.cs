using ExploreJordan.Attributes;
using ExploreJordan.Settings;

namespace ExploreJordan.ViewModels
{
    public class CreateSouvenirFromViewModel
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Double Price { get; set; }

        public Double Quantity { get; set; }


        public string Address { get; set; } = string.Empty;

        [AllowedExtensions(FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public string UserId { get; set; } = string.Empty;

        public IFormFile Cover { get; set; } = default;
    }
}
