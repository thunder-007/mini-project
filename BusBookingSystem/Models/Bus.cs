using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace BusBookingSystem.Models;
public class Bus
{
    [Key]
    public int BusId { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string BusNumber { get; set; }
    
    [Required]
    public int Capacity { get; set; }
    
    [Required]
    public int RouteId { get; set; }
    
    public Route Route { get; set; }
    
    public ICollection<Booking> Bookings { get; set; }
    
    public ICollection<Coupon> Coupons { get; set; }
}