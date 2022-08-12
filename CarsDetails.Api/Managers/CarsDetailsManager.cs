using CarsDetails.Api.Models;
using CarsDetails.Api.Repositories;
using CarsDetails.Api.Validators;

namespace CarsDetails.Api.Managers
{
    public class CarsDetailsManager : ICarsDetailsManager
    {
        private readonly ILogger<CarsDetailsManager> _logger;
        private readonly ICarsDetailsRepository _carsDetailsRepository;
        private readonly IPriceValidator _priceValidator;

        public CarsDetailsManager(ILogger<CarsDetailsManager> logger, ICarsDetailsRepository carsDetailsRepository, IPriceValidator priceValidator)
        {
            _logger = logger;
            _carsDetailsRepository = carsDetailsRepository;
            _priceValidator = priceValidator;
        }

        public async Task<CarDto> GetCarDetailsAsync(int carId)
        {
            try
            {
                var carDetails = await _carsDetailsRepository.GetCarDetailsAsync(carId);
                return carDetails;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTime.Now.ToShortTimeString()} : Exception at getting car details- {ex}");
                return null;
            }
        }

        public async Task UpdateCarDetailsAsync(CarDto car)
        {
            try
            {
                if (CheckCarValidate(car))
                {
                    await _carsDetailsRepository.UpdateCarDetailsAsync(car);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTime.Now.ToShortTimeString()} : Exception at updatring car details- {ex}");
            }
        }

        private bool CheckCarValidate(CarDto car)
        {
            var isValid = true;

            if (car.Price != null)
            {
                isValid = _priceValidator.Validate(car.Price, car.Discounts);
            }

            return isValid;
        }
    }
}
