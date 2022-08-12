using CarsCatalog.Api.Models;
using CarsCatalog.Args;
using CarsCatalog.Models;

namespace CarsCatalog.Managers
{
    public interface ICarsCatalogManager
    {
        Task AddCarAsync(CarDto car);
        Task<IEnumerable<Car>> GetCarsAsync(CarSearchArgs? args);
        Task RemoveCarAsync(int carId);
        Task UpdateCarAsync(Car car);
    }
}