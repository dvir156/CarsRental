namespace CarsCatalog.Validators
{
    public class RentDatesValidator : IRentDatesValidator
    {
        public bool Validate(DateTime? startDate, DateTime? endDate)
        {
            var result = false;

            if (startDate.HasValue && endDate.HasValue)
            {
                result = startDate.Value <= endDate.Value;
            }

            return result;
        }
    }
}
