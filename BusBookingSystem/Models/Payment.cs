using System;
using System.ComponentModel.DataAnnotations;
namespace BusBookingSystem.Models;
public class Payment
{
    [Key]
    public int PaymentId { get; set; }
    
    [Required]
    public int BookingId { get; set; }
    
    [Required]
    public decimal Amount { get; set; }
    
    [Required]
    public DateTime PaymentDate { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string PaymentMethod { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string Status { get; set; }
    
    public Booking Booking { get; set; }
}