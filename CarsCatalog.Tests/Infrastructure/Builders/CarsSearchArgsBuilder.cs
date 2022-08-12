using CarsCatalog.Args;

namespace CarsCatalog.Tests.Infrastructure.Builders
{
    internal class CarsSearchArgsBuilder
    {
        private readonly CarSearchArgs _args;
        public CarsSearchArgsBuilder()
        {
            _args = new CarSearchArgs();
        }

        public CarsSearchArgsBuilder WithRentStartDate(DateTime startDate)
        {
            _args.RentStartDate = startDate;
            return this;
        }

        public CarsSearchArgsBuilder WithRentEndDate(DateTime endDate)
        {
            _args.RentEndDate = endDate;
            return this;
        }

        public CarsSearchArgsBuilder WithDriverAge(int driverAge)
        {
            _args.DriverAge = driverAge;
            return this;
        }

        public CarsSearchArgsBuilder WithLocationsIds(IEnumerable<int> locationsIds)
        {
            _args.LocationsIds = locationsIds;
            return this;
        }

        public CarSearchArgs Build() => _args;
    }
}
