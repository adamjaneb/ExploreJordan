using ExploreJordan.Models;
using ExploreJordan.ViewModels;

namespace ExploreJordan.Services
{
    public interface IActivitiesServices
    {
        IEnumerable<Activities> GetAll();
        Activities? GetById(int id);
        Task Create(CreateActivitiesFormViewModel model);
        Task<Activities?> Edit(EditActivitiesFormViewModel model);
        bool Delete(int id);
    }
}
