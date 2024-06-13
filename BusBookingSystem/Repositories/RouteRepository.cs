using System.Collections.Generic;
using System.Linq;
using BusBookingSystem.Models;
using BusBookingSystem.Interfaces;
using BusBookingSystem.Exceptions;
using BusBookingSystem.Models;

using Route = BusBookingSystem.Models.Route;

namespace BusBookingSystem.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly ApplicationDbContext _context;

        public RouteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Route GetRouteById(int routeId) => _context.Routes.Find(routeId);

        public IEnumerable<Route> GetAllRoutes() => _context.Routes.ToList();

        public void AddRoute(Route route)
        {
            ValidateRouteTimes(route);
            _context.Routes.Add(route);
            _context.SaveChanges();
        }

        public void UpdateRoute(Route route)
        {
            ValidateRouteTimes(route);
            _context.Routes.Update(route);
            _context.SaveChanges();
        }

        public void DeleteRoute(int routeId)
        {
            var route = _context.Routes.Find(routeId);
            if (route != null)
            {
                _context.Routes.Remove(route);
                _context.SaveChanges();
            }
        }

        private void ValidateRouteTimes(Route route)
        {
            if (route.DepartureTime >= route.ArrivalTime)
            {
                throw new TimeConflictException("Time conflict: Departure time must be before arrival time.");
            }
        }
    }
}