using CarsCatalog.Tests.Infrastructure;
using CarsCatalog.Validators;

namespace CarsCatalog.Tests.Validators
{
    public class RentDatesValidatorTests
    {
        [Test]
        public void Validate_WithStartDateSmallerThenEndDate_ReturnsTrue()
        {
            using var injector = new TestsInjector();
            var validator = injector.Resolve<RentDatesValidator>();
            var startDate = DateTime.Now.AddDays(-2);
            var endDate = DateTime.Now.AddDays(2);
            var validDates = validator.Validate(startDate, endDate);

            Assert.IsTrue(validDates);
        }

        [Test]
        public void Validate_WithStartDateBiggerThenEndDate_ReturnsFalse()
        {
            using var injector = new TestsInjector();
            var validator = injector.Resolve<RentDatesValidator>();
            var startDate = DateTime.Now.AddDays(2);
            var endDate = DateTime.Now.AddDays(-2);
            var validDates = validator.Validate(startDate, endDate);

            Assert.IsFalse(validDates);
        }
    }
}
