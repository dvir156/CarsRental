namespace CarsCatalog.Validators
{
    public class DriverAgeValidator : IDriverAgeValidator
    {
        public bool Validate(int? driverAge)
        {
            var result = false;

            if (driverAge.HasValue)
            {
                result = driverAge > 0;
            }

            return result;
        }
    }
}
