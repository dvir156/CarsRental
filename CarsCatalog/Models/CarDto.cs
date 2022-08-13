using CarsCatalog.Models;

namespace CarsCatalog.Api.Models
{
    public record CarDto(int? MinimumDriverAge, IEnumerable<CarRentDates>? RentDates, IEnumerable<Location>? AvailableLocations);
}
