using System.Collections.Generic;
using BusBookingSystem.Dtos;

namespace BusBookingSystem.Interfaces
{
    public interface IBusService
    {
        BusDto GetBusById(int busId);
        IEnumerable<BusDto> GetAllBuses();
        BusDto AddBus(CreateBusDto createBusDto);
        void UpdateBus(BusDto busDto);
        void DeleteBus(int busId);
        IEnumerable<BusDto> SearchBuses(string source, string destination);
        IEnumerable<int> GetBookedSeatNumbers(int busId);  
    }
}