using CarsDetails.Api.Base;
using CarsDetails.Api.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CarsDetails.Api.Repositories
{
    public class CarsDetailsRepository : ICarsDetailsRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly string _carsKey;

        public CarsDetailsRepository(IMemoryCache memoryCache, Settings settings)
        {
            _memoryCache = memoryCache;
            _carsKey = settings.CarsKey;
        }

        public async Task<CarDto> GetCarDetailsAsync(int carId)
        {
            var result = await Task.Run(() =>
            {
                if (_memoryCache.TryGetValue(_carsKey, out CarDto car))
                {
                    return car;
                }
                else
                {
                    return null;
                }
            });

            return result;
        }

        public async Task UpdateCarDetailsAsync(CarDto car)
        {
            await Task.Run(() =>
            {
                _memoryCache.Set(_carsKey, car);
            });
        }
    }
}
