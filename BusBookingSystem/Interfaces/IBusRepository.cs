using System.Collections.Generic;
using BusBookingSystem.Models;

namespace BusBookingSystem.Interfaces
{
    public interface IBusRepository
    {
        Bus GetBusById(int busId);
        IEnumerable<Bus> GetAllBuses();
        void AddBus(Bus bus);
        void UpdateBus(Bus bus);
        void DeleteBus(int busId);
        void SaveChanges();

    }
}