namespace CarsCatalog.Models
{
    public class Car
    {
        public int Id { get; set; }
        public int MinimumDriverAge { get; set; }
        public IEnumerable<CarRentDates>? RentDates { get; set; }
        public IEnumerable<Location>? AvailableLocations { get; set; }
    }
}
