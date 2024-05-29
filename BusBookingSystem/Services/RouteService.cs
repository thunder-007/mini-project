using System.Collections.Generic;
using System.Linq;
using BusBookingSystem.Dtos;
using BusBookingSystem.Interfaces;
using BusBookingSystem.Models;
using Route = BusBookingSystem.Models.Route;

namespace BusBookingSystem.Services;
public class RouteService : IRouteService
{
    private readonly IRouteRepository _routeRepository;

    public RouteService(IRouteRepository routeRepository)
    {
        _routeRepository = routeRepository;
    }

    public RouteDto GetRouteById(int routeId)
    {
        var route = _routeRepository.GetRouteById(routeId);
        if (route == null) return null;

        return new RouteDto
        {
            RouteId = route.RouteId,
            Source = route.Source,
            Destination = route.Destination,
            DepartureTime = route.DepartureTime,
            ArrivalTime = route.ArrivalTime
        };
    }

    public IEnumerable<RouteDto> GetAllRoutes()
    {
        var routes = _routeRepository.GetAllRoutes();
        return routes.Select(route => new RouteDto
        {
            RouteId = route.RouteId,
            Source = route.Source,
            Destination = route.Destination,
            DepartureTime = route.DepartureTime,
            ArrivalTime = route.ArrivalTime
        }).ToList();
    }

    public Route AddRoute(CreateRouteDto createRouteDto)
    {
        var route = new Route
        {
            Source = createRouteDto.Source,
            Destination = createRouteDto.Destination,
            DepartureTime = createRouteDto.DepartureTime,
            ArrivalTime = createRouteDto.ArrivalTime
        };

        _routeRepository.AddRoute(route);
        return route;
    }

    public void UpdateRoute(int routeId, UpdateRouteDto updateRouteDto)
    {
        var route = _routeRepository.GetRouteById(routeId);
        if (route == null) return;

        route.Source = updateRouteDto.Source;
        route.Destination = updateRouteDto.Destination;
        route.DepartureTime = updateRouteDto.DepartureTime;
        route.ArrivalTime = updateRouteDto.ArrivalTime;

        _routeRepository.UpdateRoute(route);
    }

    public void DeleteRoute(int routeId)
    {
        _routeRepository.DeleteRoute(routeId);
    }
}
