using ExploreJordan.Models;
using ExploreJordan.ViewModels;

namespace ExploreJordan.Services
{
    public interface IToursServices
    {
        IEnumerable<Tours> GetAll();
        Tours? GetById(int id);
        Task Create(CreateToursFormViewModel model);
        Task<Tours?> Edit(EditToursFormViewModel model);
        bool Delete(int id);
    }
}
