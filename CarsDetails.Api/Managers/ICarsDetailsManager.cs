using CarsDetails.Api.Models;

namespace CarsDetails.Api.Managers
{
    public interface ICarsDetailsManager
    {
        Task<CarDto> GetCarDetailsAsync(int carId);
        Task UpdateCarDetailsAsync(CarDto car);
    }
}