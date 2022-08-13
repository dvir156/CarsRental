using AutoMapper;
using CarsCatalog.Api.Models;
using CarsCatalog.Args;
using CarsCatalog.Models;
using CarsCatalog.Repositories;
using CarsCatalog.Validators;

namespace CarsCatalog.Managers
{
    public class CarsCatalogManager : ICarsCatalogManager
    {
        private readonly ILogger<CarsCatalogManager> _logger;
        private readonly ICarsCatalogRepository _carsCatalogRepository;
        private readonly IMapper _mapper;
        private readonly IRentDatesValidator _rentDatesValidator;
        private readonly IDriverAgeValidator _driverAgeValidator;

        public CarsCatalogManager(ILogger<CarsCatalogManager> logger, ICarsCatalogRepository carsCatalogRepository, IMapper mapper,
            IRentDatesValidator rentDatesValidator, IDriverAgeValidator driverAgeValidator)
        {
            _logger = logger;
            _carsCatalogRepository = carsCatalogRepository;
            _mapper = mapper;
            _rentDatesValidator = rentDatesValidator;
            _driverAgeValidator = driverAgeValidator;
        }
        public async Task<IEnumerable<Car>> GetCarsAsync(CarSearchArgs? args)
        {
            try
            {
                var cars = await _carsCatalogRepository.GetAllCars();

                FilterCarsBySearchArgs(ref cars, args);

                return cars;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTime.Now.ToShortTimeString()} : Exception at getting cars - {ex}");
                return Enumerable.Empty<Car>();
            }
        }
        public async Task AddCarAsync(CarDto car)
        {
            try
            {
                var carModel = _mapper.Map<Car>(car);

                if (IsCarValid(carModel))
                {
                    await _carsCatalogRepository.AddCarAsync(carModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTime.Now.ToShortTimeString()} : Exception at adding car - {ex}");
            }
        }

        private bool IsCarValid(Car car)
        {
            var result = true;

            if (car.RentDates != null)
            {
                foreach (var rentDate in car.RentDates)
                {
                    if(!_rentDatesValidator.Validate(rentDate.StartDate, rentDate.EndDate))
                    {
                        return false;  
                    }
                }
            }

            if (car.MinimumDriverAge != null)
            {
                result = _driverAgeValidator.Validate(car.MinimumDriverAge);
            }

            return result;
        }

        public async Task RemoveCarAsync(int carId)
        {
            try
            {
                await _carsCatalogRepository.RemoveCarAsync(carId);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTime.Now.ToShortTimeString()} : Exception at remove car - {ex}");
            }
        }

        public async Task UpdateCarAsync(Car car)
        {
            try
            {
                if (IsCarValid(car))
                {
                    await _carsCatalogRepository.UpdateCarAsync(car);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTime.Now.ToShortTimeString()} : Exception at update car - {ex}");
            }
        }

        private void FilterCarsBySearchArgs(ref IEnumerable<Car> cars, CarSearchArgs? args)
        {
            if (args != null)
            {
                FilterByRentDates(ref cars, args.RentStartDate, args.RentEndDate);
                FilterByDriverAge(ref cars, args.DriverAge);
                FilterByLocationIds(ref cars, args.LocationsIds);
            }
        }

        private void FilterByDriverAge(ref IEnumerable<Car> cars, int? driverAge)
        {
            if (_driverAgeValidator.Validate(driverAge))
            {
                cars = cars.Where(c => c.MinimumDriverAge <= driverAge);
            }
        }

        private void FilterByRentDates(ref IEnumerable<Car> cars, DateTime? rentStartDate, DateTime? rentEndDate)
        {
            if (_rentDatesValidator.Validate(rentStartDate, rentEndDate))
            {
                cars = cars.Where(c => !c.RentDates.Any(d => IsCarRent((DateTime)rentStartDate, (DateTime)rentEndDate, d.StartDate, d.EndDate)));
            }
        }
        private bool IsCarRent(DateTime rentStartDate, DateTime rentEndDate, DateTime checkStartDate, DateTime checkEndDate)
        {
            return rentEndDate >= checkStartDate && rentStartDate <= checkEndDate;
        }

        private void FilterByLocationIds(ref IEnumerable<Car> cars, IEnumerable<int>? locations)
        {
            if (locations != null)
            {
                cars = cars.Where(c => c.AvailableLocations.Any(l => locations.Contains(l.Id)));
            }
        }


    }
}
