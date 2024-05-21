namespace BusBookingSystem.Models
{
    public class Bus 
    {
        public int BusId { get; set; }
        public string BusNumber { get; set; }
        public int Capacity { get; set; }
        public int RouteId { get; set; }
        public Route Route { get; set; }
    }
}
