using CarsCatalog.Tests.Infrastructure;
using CarsCatalog.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsCatalog.Tests.Validators
{
    internal class DriverAgeValidatorTests
    {
        [Test]
        public void Validate_WithDriverSmallerThenZero_ReturnsFalse()
        {
            using var injector = new TestsInjector();
            var validator = injector.Resolve<DriverAgeValidator>();

            Assert.IsFalse(validator.Validate(-5));
        }

        [Test]
        public void Validate_WithDrvierAgeBiggerThenZero_ReturnsTrue()
        {
            using var injector = new TestsInjector();
            var validator = injector.Resolve<DriverAgeValidator>();

            Assert.IsTrue(validator.Validate(5));
        }
    }
}
