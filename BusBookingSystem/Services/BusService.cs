using System.Collections.Generic;
using System.Linq;
using BusBookingSystem.Models;
using BusBookingSystem.Interfaces;
using BusBookingSystem.Dtos;

namespace BusBookingSystem.Services
{
    public class BusService : IBusService
    {
        private readonly IBusRepository _busRepository;

        public BusService(IBusRepository busRepository)
        {
            _busRepository = busRepository;
        }

        public BusDto GetBusById(int busId)
        {
            var bus = _busRepository.GetBusById(busId);
            return bus != null ? MapToDto(bus) : null;
        }

        public IEnumerable<BusDto> GetAllBuses()
        {
            var buses = _busRepository.GetAllBuses();
            return buses.Select(b => MapToDto(b));
        }

        public BusDto AddBus(CreateBusDto createBusDto)
        {
            var bus = new Bus
            {
                BusNumber = createBusDto.BusNumber,
                Capacity = createBusDto.Capacity,
                RouteId = createBusDto.RouteId
            };

            _busRepository.AddBus(bus);
            _busRepository.SaveChanges();

            return MapToDto(bus);
        }


        public void UpdateBus(BusDto busDto)
        {
            var bus = MapToEntity(busDto);
            _busRepository.UpdateBus(bus);
        }

        public void DeleteBus(int busId)
        {
            _busRepository.DeleteBus(busId);
        }

        private BusDto MapToDto(Bus bus)
        {
            return new BusDto
            {
                BusId = bus.BusId,
                BusNumber = bus.BusNumber,
                Capacity = bus.Capacity,
                RouteId = bus.RouteId
            };
        }

        private Bus MapToEntity(CreateBusDto createBusDto)
        {
            return new Bus
            {
                BusNumber = createBusDto.BusNumber,
                Capacity = createBusDto.Capacity,
                RouteId = createBusDto.RouteId
            };
        }

        private Bus MapToEntity(BusDto busDto)
        {
            return new Bus
            {
                BusId = busDto.BusId,
                BusNumber = busDto.BusNumber,
                Capacity = busDto.Capacity,
                RouteId = busDto.RouteId
            };
        }
    }
}
