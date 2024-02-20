using System.ComponentModel.DataAnnotations;

namespace ExploreJordan.Models
{
    public class Souvenirs
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Double Price { get; set; }
        public Double Quantity { get; set; }
        public string Cover { get; set; } = string.Empty;
        public string UserId { get; internal set; }

    }
}
