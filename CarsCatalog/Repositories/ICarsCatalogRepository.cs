using CarsCatalog.Api.Models;
using CarsCatalog.Models;

namespace CarsCatalog.Repositories
{
    public interface ICarsCatalogRepository
    {
        Task AddCarAsync(Car car);
        Task RemoveCarAsync(int carId);
        Task UpdateCarAsync(Car car);
        Task<IEnumerable<Car>> GetAllCars();
    }
}
