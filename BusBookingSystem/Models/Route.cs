using System;
using System.ComponentModel.DataAnnotations;

namespace BusBookingSystem.Models;
public class Route
{
    [Key]
    public int RouteId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Source { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Destination { get; set; }
    
    [Required]
    public DateTime DepartureTime { get; set; }
    
    [Required]
    public DateTime ArrivalTime { get; set; }
    
    public ICollection<Bus> Buses { get; set; }
}