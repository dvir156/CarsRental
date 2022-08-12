using CarsCatalog.Managers;
using CarsCatalog.Models;
using CarsCatalog.Repositories;
using CarsCatalog.Tests.Infrastructure;
using CarsCatalog.Tests.Infrastructure.Builders;
using CarsCatalog.Validators;
using FakeItEasy;

namespace CarsCatalog.Tests.Managers
{
    internal class CarsCatalogManagerTests
    {
        [Test]
        public void GetCars_WithArgsContainsMultipleParametersThatNotMatch_ReturnEmpty()
        {
            using var injector = new TestsInjector();
            var fakeRepository = injector.Resolve<ICarsCatalogRepository>();
            var rentDatesValidator = injector.Resolve<IRentDatesValidator>();
            var driverAgeValidator = injector.Resolve<IDriverAgeValidator>();


            A.CallTo(() =>
                fakeRepository.GetAllCars()
            ).Returns(new List<Car> { new Car() {
                AvailableLocations = new List<Location>() { new Location() { Id = 1 } },
                MinimumDriverAge = 6,
                RentDates = new List<CarRentDates>() { new CarRentDates() { StartDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(4) } }} });

            A.CallTo(() =>
              rentDatesValidator.Validate(A<DateTime>._, A<DateTime>._)).Returns(true);


            A.CallTo(() =>
             driverAgeValidator.Validate(A<int>._)).Returns(true);

            var args = new CarsSearchArgsBuilder()
                .WithLocationsIds(new[] { 2 })
                .WithRentStartDate(DateTime.Now)
                .WithRentEndDate(DateTime.Now.AddDays(12))
                .WithDriverAge(2)
                .Build();

            var carsCatalogManager = injector.Resolve<CarsCatalogManager>();
            var cars = carsCatalogManager.GetCarsAsync(args).Result;

            Assert.That(cars, Is.Empty);
        }

        [Test]
        public void GetCars_WithArgsContainsMultipleParametersThatMatch_ReturnCars()
        {
            using var injector = new TestsInjector();
            var fakeRepository = injector.Resolve<ICarsCatalogRepository>();
            var rentDatesValidator = injector.Resolve<IRentDatesValidator>();
            var driverAgeValidator = injector.Resolve<IDriverAgeValidator>();


            A.CallTo(() =>
                fakeRepository.GetAllCars()
            ).Returns(new List<Car> { new Car() { 
                AvailableLocations = new List<Location>() { new Location() { Id = 1 } },
                MinimumDriverAge = 6,
                RentDates = new List<CarRentDates>() { new CarRentDates() { StartDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(4) } }} });

            A.CallTo(() =>
              rentDatesValidator.Validate(A<DateTime>._, A<DateTime>._)).Returns(true);


            A.CallTo(() =>
             driverAgeValidator.Validate(A<int>._)).Returns(true);

            var args = new CarsSearchArgsBuilder()
                .WithLocationsIds(new[] { 1 })
                .WithRentStartDate(DateTime.Now.AddDays(10))
                .WithRentEndDate(DateTime.Now.AddDays(12))
                .WithDriverAge(19)
                .Build();

            var carsCatalogManager = injector.Resolve<CarsCatalogManager>();
            var cars = carsCatalogManager.GetCarsAsync(args).Result;

            Assert.That(cars, Is.Not.Empty);
        }

        [Test]
        public void GetCars_WithArgsContainsSmallerDriverAgeParameters_ReturnEmpty()
        {
            using var injector = new TestsInjector();
            var fakeRepository = injector.Resolve<ICarsCatalogRepository>();
            var driverAgeValidator = injector.Resolve<IDriverAgeValidator>();

            A.CallTo(() =>
                fakeRepository.GetAllCars()
            ).Returns(new List<Car> { new Car() {MinimumDriverAge = 12 } });

            A.CallTo(() =>
               driverAgeValidator.Validate(A<int>._)).Returns(true);

            var args = new CarsSearchArgsBuilder()
                .WithDriverAge(6)
                .Build();

            var carsCatalogManager = injector.Resolve<CarsCatalogManager>();
            var cars = carsCatalogManager.GetCarsAsync(args).Result;

            Assert.That(cars, Is.Empty);
        }

        [Test]
        public void GetCars_WithArgsContainsBiggerDriverAgeParameters_ReturnCars()
        {
            using var injector = new TestsInjector();
            var fakeRepository = injector.Resolve<ICarsCatalogRepository>();
            var driverAgeValidator = injector.Resolve<IDriverAgeValidator>();

            A.CallTo(() =>
                fakeRepository.GetAllCars()
            ).Returns(new List<Car> { new Car() { MinimumDriverAge = 6 } });

            A.CallTo(() =>
              driverAgeValidator.Validate(A<int>._)).Returns(true);

            var args = new CarsSearchArgsBuilder()
                .WithDriverAge(12)
                .Build();

            var carsCatalogManager = injector.Resolve<CarsCatalogManager>();
            var cars = carsCatalogManager.GetCarsAsync(args).Result;

            Assert.That(cars, Is.Not.Empty);
        }

        [Test]
        public void GetCars_WithArgsContainsStartDateAndEndDateAvailableParameters_ReturnEmpty()
        {
            using var injector = new TestsInjector();
            var fakeRepository = injector.Resolve<ICarsCatalogRepository>();
            var rentDatesValidator = injector.Resolve<IRentDatesValidator>();

            A.CallTo(() =>
                fakeRepository.GetAllCars()
            ).Returns(new List<Car> { new Car() { RentDates = new List<CarRentDates>() { new CarRentDates() { StartDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(4) } } } });

            A.CallTo(() =>
               rentDatesValidator.Validate(A<DateTime>._, A<DateTime>._)).Returns(true);

            var args = new CarsSearchArgsBuilder()
                .WithRentStartDate(DateTime.Now)
                .WithRentEndDate(DateTime.Now.AddDays(2))
                .Build();

            var carsCatalogManager = injector.Resolve<CarsCatalogManager>();
            var cars = carsCatalogManager.GetCarsAsync(args).Result;

            Assert.That(cars, Is.Empty);
        }

        [Test]
        public void GetCars_WithArgsContainsStartDateAndEndDateNotAvailableParameters_ReturnCars()
        {
            using var injector = new TestsInjector();
            var fakeRepository = injector.Resolve<ICarsCatalogRepository>();
            var rentDatesValidator = injector.Resolve<IRentDatesValidator>();

            A.CallTo(() =>
                fakeRepository.GetAllCars()
            ).Returns(new List<Car> { new Car() { RentDates = new List<CarRentDates>() { new CarRentDates() { StartDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(4) } } } });

            A.CallTo(() =>
               rentDatesValidator.Validate(A<DateTime>._, A<DateTime>._)).Returns(true);

            var args = new CarsSearchArgsBuilder()
                .WithRentStartDate(DateTime.Now.AddDays(5))
                .WithRentEndDate(DateTime.Now.AddDays(7))
                .Build();

            var carsCatalogManager = injector.Resolve<CarsCatalogManager>();
            var cars = carsCatalogManager.GetCarsAsync(args).Result;

            Assert.That(cars, Is.Not.Empty);
        }

        [Test]
        public void GetCars_WithArgsContainsLocationAvailableParameters_ReturnEmpty()
        {
            using var injector = new TestsInjector();
            var fakeRepository = injector.Resolve<ICarsCatalogRepository>();

            A.CallTo(() =>
                fakeRepository.GetAllCars()
            ).Returns(new List<Car> { new Car() { AvailableLocations = new List<Location>() { new Location() { Id = 1 } } } });


            var args = new CarsSearchArgsBuilder()
                .WithLocationsIds(new[] { 2 })
                .Build();

            var carsCatalogManager = injector.Resolve<CarsCatalogManager>();
            var cars = carsCatalogManager.GetCarsAsync(args).Result;

            Assert.That(cars, Is.Empty);
        }

        [Test]
        public void GetCars_WithArgsContainsLocationNotAvailableParameters_ReturnCars()
        {
            using var injector = new TestsInjector();
            var fakeRepository = injector.Resolve<ICarsCatalogRepository>();

            A.CallTo(() =>
                fakeRepository.GetAllCars()
            ).Returns(new List<Car> { new Car() { AvailableLocations = new List<Location>() { new Location() { Id = 1 } } } });


            var args = new CarsSearchArgsBuilder()
                .WithLocationsIds(new[] { 1 })
                .Build();

            var carsCatalogManager = injector.Resolve<CarsCatalogManager>();
            var cars = carsCatalogManager.GetCarsAsync(args).Result;


            Assert.That(cars, Is.Not.Empty);
        }
    }
}
