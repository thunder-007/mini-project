using System.Collections.Generic;
using System.Linq;
using BusBookingSystem.Models;
using BusBookingSystem.Interfaces;

namespace BusBookingSystem.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }
        public int GetBookingsCountByBusId(int busId)
        {
            return _context.Bookings.Count(b => b.BusId == busId);
        }
        public Booking GetBookingByBusIdAndSeatNumber(int busId, int seatNumber)
        {
            return _context.Bookings.FirstOrDefault(b => b.BusId == busId && b.SeatNumber == seatNumber);
        }
        public IEnumerable<Booking> GetBookedSeatsByBusId(int busId)
        {
            return _context.Bookings.Where(b => b.BusId == busId).ToList();
        }



    }
}