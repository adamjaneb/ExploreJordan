using ExploreJordan.Models;
using ExploreJordan.ViewModels;

namespace ExploreJordan.Services
{
	public interface ICarsServices
	{
		IEnumerable<Car> GetAll();
		Car? GetById(int id);	
		Task Create(CreateFormViewModel model);
        Task <Car?> Edit(EditCarFormViewModel model);
		bool Delete(int id);

    }
}
