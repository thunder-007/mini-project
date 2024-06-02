using System.Collections.Generic;
using System.Linq;
using BusBookingSystem.Models;
using Microsoft.EntityFrameworkCore;
using BusBookingSystem.Interfaces;

namespace BusBookingSystem.Repositories
{
    public class BusRepository : IBusRepository
    {
        private readonly ApplicationDbContext _context;

        public BusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Bus GetBusById(int busId)
        {
            return _context.Buses
                .Include(b => b.Route)
                .FirstOrDefault(b => b.BusId == busId);
        }


        public IEnumerable<Bus> GetAllBuses() => _context.Buses.ToList();

        public void AddBus(Bus bus)
        {
            _context.Buses.Add(bus);
            _context.SaveChanges();
        }

        public void UpdateBus(Bus bus)
        {
            _context.Buses.Update(bus);
            _context.SaveChanges();
        }

        public void DeleteBus(int busId)
        {
            var bus = _context.Buses.Find(busId);
            if (bus != null)
            {
                _context.Buses.Remove(bus);
                _context.SaveChanges();
            }
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}