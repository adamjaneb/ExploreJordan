using ExploreJordan.Models;
using ExploreJordan.ViewModels;

namespace ExploreJordan.Services
{
    public interface ISouvenirServices
    {
        IEnumerable<Souvenirs> GetAll();
        Souvenirs? GetById(int id);
        Task Create(CreateSouvenirFromViewModel model);
        Task<Souvenirs?> Edit(EditSouvenirFormViewModel model);
        bool Delete(int id);

    }
}
