using System.ComponentModel.DataAnnotations;

namespace BusBookingSystem.Models;
public class Admin
{
    [Key]
    public int AdminId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string AdminName { get; set; }
    
    [Required]
    public string PasswordHash { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}