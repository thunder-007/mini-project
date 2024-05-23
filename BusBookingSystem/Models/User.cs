using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusBookingSystem.Models;
public class User
{
    [Key]
    public int UserId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string UserName { get; set; }
    
    [Required]
    public string PasswordHash { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string Role { get; set; }
    
    public ICollection<Booking> Bookings { get; set; }
}