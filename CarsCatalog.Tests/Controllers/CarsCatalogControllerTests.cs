using CarsCatalog.Api.Models;
using CarsCatalog.Controllers;
using CarsCatalog.Managers;
using CarsCatalog.Models;
using CarsCatalog.Tests.Infrastructure;
using CarsCatalog.Tests.Infrastructure.Builders;
using FakeItEasy;

namespace CarsCatalog.Tests.Controllers
{
    internal class CarsCatalogControllerTests
    {
        [Test]
        public void GetCars_WithDefaultSearchArgs_ForwardsArgsToCarsCatalogManagerGetCarsMethod()
        {
            using var injector = new TestsInjector();
            var fakeCarsCatalogManager = injector.Resolve<ICarsCatalogManager>();

            var args = new CarsSearchArgsBuilder().Build();
            var controller = injector.Resolve<CarsCatalogController>();
            _ = controller.GetCarsAsync(args);

            A.CallTo(() =>
                fakeCarsCatalogManager.GetCarsAsync(args)
            ).MustHaveHappenedOnceExactly();
        }
        [Test]
        public void AddCar_WithDefaultCarDtoArgs_ForwardsArgsToCarsCatalogManagerAddCarMethod()
        {
            using var injector = new TestsInjector();
            var fakeCarsCatalogManager = injector.Resolve<ICarsCatalogManager>();
            var carDto = new CarDto(20, null, null);
            var controller = injector.Resolve<CarsCatalogController>();
            _ = controller.AddCarAsync(carDto);

            A.CallTo(() =>
                fakeCarsCatalogManager.AddCarAsync(carDto)
            ).MustHaveHappenedOnceExactly();
        }
        [Test]
        public void UpdateCars_WithCarArgs_ForwardsArgsToCarsCatalogManagerUpdateCarMethod()
        {
            using var injector = new TestsInjector();
            var fakeCarsCatalogManager = injector.Resolve<ICarsCatalogManager>();
            var car = new Car();
            var controller = injector.Resolve<CarsCatalogController>();
            _ = controller.UpdateCarAsync(car);

            A.CallTo(() =>
                fakeCarsCatalogManager.UpdateCarAsync(car)
            ).MustHaveHappenedOnceExactly();
        }
        [Test]
        public void DeleteCars_WithCarId_ForwardsArgsToCarsCatalogManagerDeleteCarMethod()
        {
            using var injector = new TestsInjector();
            var fakeCarsCatalogManager = injector.Resolve<ICarsCatalogManager>();
            var controller = injector.Resolve<CarsCatalogController>();
            _ = controller.RemoveCarAsync(1);

            A.CallTo(() =>
                fakeCarsCatalogManager.RemoveCarAsync(1)
            ).MustHaveHappenedOnceExactly();
        }
    }
}
