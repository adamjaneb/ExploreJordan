using ExploreJordan.Models;
using ExploreJordan.ViewModels;

namespace ExploreJordan.Services
{
    public interface IAccommodationServices
    {
        IEnumerable<Accommodation> GetAll();
        Accommodation? GetById(int id);
        Task Create(CreateAccommodationFormViewModel model);
        Task<Accommodation?> Edit(EditAccommodationFormViewModel model);
        bool Delete(int id);

    }
}
