using CarsDetails.Api.Validators;
using CarsDetails.Tests.Infrastructure;

namespace CarsDetails.Tests.Validators
{
    internal class PriceVaildatorTests
    {
        [Test]
        public void Validate_WithPriceSmallerThenZero_ReturnsFalse()
        {
            using var injector = new TestsInjector();
            var validator = injector.Resolve<PriceValidator>();

            Assert.IsFalse(validator.Validate(-5, null));
        }

        [Test]
        public void Validate_WithPriceBiggerThenZero_ReturnsTrue()
        {
            using var injector = new TestsInjector();
            var validator = injector.Resolve<PriceValidator>();

            Assert.IsTrue(validator.Validate(5, null));
        }

        [Test]
        public void Validate_WithPriceSmallerThenDiscont_ReturnsFalse()
        {
            using var injector = new TestsInjector();
            var validator = injector.Resolve<PriceValidator>();

            Assert.IsFalse(validator.Validate(5, 6));
        }

        [Test]
        public void Validate_WithPriceBiggerThenDiscont_ReturnsTrue()
        {
            using var injector = new TestsInjector();
            var validator = injector.Resolve<PriceValidator>();

            Assert.IsTrue(validator.Validate(5, 4));
        }
    }
}
