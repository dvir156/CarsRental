namespace CarsCatalog.Validators
{
    public interface IRentDatesValidator
    {
        bool Validate(DateTime? startDate, DateTime? endDate);
    }
}