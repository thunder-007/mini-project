namespace BusBookingSystem.Dtos;

public class CreateRouteDto
{
    public string Source { get; set; }

    public string Destination { get; set; }

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }
}