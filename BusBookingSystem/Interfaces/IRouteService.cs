using System.Collections.Generic;
using BusBookingSystem.Dtos;
using BusBookingSystem.Models;
using Route = BusBookingSystem.Models.Route;

namespace BusBookingSystem.Interfaces
{
    public interface IRouteService
    {
        RouteDto GetRouteById(int routeId);
        IEnumerable<RouteDto> GetAllRoutes();
        Route AddRoute(CreateRouteDto createRouteDto);
        void UpdateRoute(int routeId, UpdateRouteDto updateRouteDto);
        void DeleteRoute(int routeId);
    }
}