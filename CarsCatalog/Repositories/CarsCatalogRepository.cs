using CarsCatalog.Api.Base;
using CarsCatalog.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CarsCatalog.Repositories
{
    public class CarsCatalogRepository : ICarsCatalogRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly string _carsKey;
        private MemoryCacheEntryOptions? _cacheExpiryOptions;
        private int _idGenrator;
        public CarsCatalogRepository(IMemoryCache memoryCache, Settings settings)
        {
            _memoryCache = memoryCache;
            _carsKey = settings.CarsKey;
            SetMemoryCach();
        }

        private void SetMemoryCach()
        {
            if (!_memoryCache.TryGetValue(_carsKey, out IEnumerable<Car> _))
            {
                _cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(3),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(2),
                    Size = 1024,
                };
                _memoryCache.Set(_carsKey, new List<Car>(), _cacheExpiryOptions);
            }
        }

        public async Task AddCarAsync(Car car)
        {
            await Task.Run(() =>
            {
                if (_memoryCache.TryGetValue(_carsKey, out List<Car> cars))
                {
                    car.Id = ++_idGenrator;
                    cars.Add(car);
                    _memoryCache.Set(_carsKey, cars, _cacheExpiryOptions);
                }
            });
        }

        public async Task<IEnumerable<Car>> GetAllCars()
        {
            var cars = await Task.Run(() =>
            {
                if (_memoryCache.TryGetValue(_carsKey, out IEnumerable<Car> cars))
                {
                    return cars;
                }
                else
                {
                    return Enumerable.Empty<Car>();
                }
            });

            return cars;
        }

        public async Task RemoveCarAsync(int carId)
        {
            await Task.Run(() =>
            {
                if (_memoryCache.TryGetValue(_carsKey, out List<Car> cars))
                {
                    cars.RemoveAll(c => c.Id == carId);
                    _memoryCache.Set(_carsKey, cars, _cacheExpiryOptions);
                }
            });
        }

        public async Task UpdateCarAsync(Car car)
        {
            await Task.Run(() =>
            {
                if (_memoryCache.TryGetValue(_carsKey, out List<Car> cars))
                {
                    cars.RemoveAll(c => c.Id == car.Id);
                    cars.Add(car);
                    _memoryCache.Set(_carsKey, cars, _cacheExpiryOptions);
                }
            });
        }
    }
}
