namespace CarsDetails.Api.Validators
{
    public class PriceValidator : IPriceValidator
    {
        public bool Validate(int? price, int? discont)
        {
            var result = false;

            if (price.HasValue && discont.HasValue)
            {
                result = price > 0 && discont < price && discont > 0;
            }
            else if (price.HasValue)
            {
                result = price > 0;
            }

            return result;
        }
    }
}
