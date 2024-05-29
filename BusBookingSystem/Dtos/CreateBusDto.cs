namespace BusBookingSystem.Dtos
{
    public class CreateBusDto
    {
        public string BusNumber { get; set; }
        public int Capacity { get; set; }
        public int RouteId { get; set; }
    }
}