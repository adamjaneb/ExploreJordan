using ExploreJordan.Attributes;
using ExploreJordan.Settings;
using Microsoft.AspNetCore.Http;

namespace ExploreJordan.ViewModels
{
	public class CreateFormViewModel : CarFormViewModel

	{
		[AllowedExtensions(FileSettings.AllowedExtensions),
			MaxFileSize(FileSettings.MaxFileSizeInBytes)]
		public string UserId { get; set; } = string.Empty;

		public string Address { get; set; } = string.Empty;

		public IFormFile Cover { get; set; } = default;


	}
}
