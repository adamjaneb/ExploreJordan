using System.ComponentModel.DataAnnotations;
using ExploreJordan.Models;

namespace ExploreJordan.ViewModels
{
    public class CombinedViewModel
    {
        public IEnumerable<Car>? Cars { get; set; }
        public IEnumerable<Activities>? Activities { get; set; }
        public IEnumerable<Tours>? Tours { get; set; }
        public IEnumerable<Souvenirs>? Souvenirs { get; set; }
        public IEnumerable<Accommodation>? Accommodations { get; set; }

    }
}
