using System.Collections.Generic;
using BusBookingSystem.Models;

namespace BusBookingSystem.Interfaces
{
    public interface IBookingRepository
    {
        void CreateBooking(Booking booking);

        // Booking GetBookingById(int bookingId);
        // IEnumerable<Booking> GetAllBookings();
        // void AddBooking(Booking booking);
        // void UpdateBooking(Booking booking);
        // void DeleteBooking(int bookingId);
    }
}