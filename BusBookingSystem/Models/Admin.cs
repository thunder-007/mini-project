namespace BusBookingSystem.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
    }
}
