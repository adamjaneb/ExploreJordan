using ExploreJordan.Attributes;
using ExploreJordan.Settings;

namespace ExploreJordan.ViewModels
{
    public class EditSouvenirFormViewModel
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Double Price { get; set; }

        public Double Quantity { get; set; }

        public IFormFile? Cover { get; set; } = default;
        public int Id { get; internal set; }
    }
}
