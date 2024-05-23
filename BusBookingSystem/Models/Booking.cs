using System;
using System.ComponentModel.DataAnnotations;
namespace BusBookingSystem.Models; 
public class Booking
{
    [Key]
    public int BookingId { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public int BusId { get; set; }
    
    [Required]
    public DateTime BookingDate { get; set; }
    
    [Required]
    public int SeatNumber { get; set; }
    
    public User User { get; set; }
    
    public Bus Bus { get; set; }
    
    public Payment Payment { get; set; }
}