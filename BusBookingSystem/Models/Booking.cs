namespace BusBookingSystem.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BusId { get; set; }
        public Bus Bus { get; set; }
        public DateTime BookingDate { get; set; }
        public int SeatNumber { get; set; }
    }
}
