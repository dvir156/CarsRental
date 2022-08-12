namespace CarsCatalog.Models
{
    public class CarRentDates
    {
        public int Id { get; set; }
        public int UserRentId { get; set; }    
        public DateTime StartDate { get; set; }    
        public DateTime EndDate { get; set; }
    }
}
