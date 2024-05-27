using System.Collections.Generic;
using BusBookingSystem.Models;
using Route = BusBookingSystem.Models.Route;

namespace BusBookingSystem.Interfaces
{
    public interface IRouteRepository
    {
        Route GetRouteById(int routeId);
        IEnumerable<Route> GetAllRoutes();
        void AddRoute(Route route);
        void UpdateRoute(Route route);
        void DeleteRoute(int routeId);
    }
}