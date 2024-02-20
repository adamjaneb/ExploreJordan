using System.ComponentModel.DataAnnotations;

namespace GradProject.Models
{
	public class Traveler
	{
		[Key]
		public int TravelerID { get; set; }
		[Required]
        public string Name { get; set; }
		[Required]
        public string ContactInfo { get; set; }
    }
}
