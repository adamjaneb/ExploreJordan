namespace ExploreJordan.Settings
{
	public static class FileSettings
	{
		public const string ImagesPath = "/assets/images/cars";
		public const string AllowedExtensions = ".jpg,.jpeg,.png";
		public const int MaxFileSizeInMb = 1;
		public const int MaxFileSizeInBytes = MaxFileSizeInMb * 1024 * 1024;
	}
}
