using System.ComponentModel.DataAnnotations;

namespace ExploreJordan.Models
{
	public class ServiceProvider
	{
		[Key]
		public int ProviderID { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string ContactInfo { get; set; }
	}
}
