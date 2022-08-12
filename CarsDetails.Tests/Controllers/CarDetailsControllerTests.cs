using CarsDetails.Api.Controllers;
using CarsDetails.Api.Managers;
using CarsDetails.Tests.Infrastructure;
using FakeItEasy;

namespace CarsDetails.Tests.Controllers
{
    internal class CarDetailsControllerTests
    {
        [Test]
        public void GetCar_WithId_ForwardsIdToCarsDetailsManagerGetCarsDetailsAsyncMethod()
        {
            using var injector = new TestsInjector();
            var fakeCarsDetailsManager = injector.Resolve<ICarsDetailsManager>();

            var controller = injector.Resolve<CarDetailsController>();
            _ = controller.GetCarDetailsAsync(1);

            A.CallTo(() =>
                           fakeCarsDetailsManager.GetCarDetailsAsync(1)
                       ).MustHaveHappened();
        }

        [Test]
        public void UpdateCar_WithCarDtoParameters_ForwardsParametersToCarsDetailsManagerUpdateCarMethod()
        {
            using var injector = new TestsInjector();
            var fakeCarsDetailsManager = injector.Resolve<ICarsDetailsManager>();
            var carDto = new Api.Models.CarDto("test", null, null, null);
            var controller = injector.Resolve<CarDetailsController>();
            _ = controller.UpdateCarDetailsAsync(carDto);

            A.CallTo(() =>
                           fakeCarsDetailsManager.UpdateCarDetailsAsync(carDto)
                       ).MustHaveHappened();
        }
    }
}
