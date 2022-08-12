using CarsDetails.Api.Models;

namespace CarsDetails.Api.Repositories
{
    public interface ICarsDetailsRepository
    {
        Task<CarDto> GetCarDetailsAsync(int carId);
        Task UpdateCarDetailsAsync(CarDto car);
    }
}