namespace CarsDetails.Api.Models
{
    public record CarDto(string? Description, IEnumerable<string>? AvailableExtras, int? Price, int? Discounts);
}
