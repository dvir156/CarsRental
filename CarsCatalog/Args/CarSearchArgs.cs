using CarsCatalog.Models;

namespace CarsCatalog.Args
{
    public class CarSearchArgs
    {
        public DateTime? RentStartDate { get; set; }
        public DateTime? RentEndDate { get; set; }
        public IEnumerable<int>? LocationsIds { get; set; }
        public int? DriverAge { get; set; }
    }
}
