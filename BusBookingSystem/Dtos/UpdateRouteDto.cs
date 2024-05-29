namespace BusBookingSystem.Dtos;
using System.ComponentModel.DataAnnotations;


public class UpdateRouteDto
{
    public string Source { get; set; }
    public string Destination { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
}
