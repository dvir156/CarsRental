namespace CarsDetails.Api.Validators
{
    public interface IPriceValidator
    {
        bool Validate(int? price, int? discont);
    }
}