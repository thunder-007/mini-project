using System;
using System.Linq;
using Xunit;
using BusBookingSystem.Models;
using BusBookingSystem.Dtos;
using BusBookingSystem.Services;
using BusBookingSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using Route = BusBookingSystem.Models.Route;

namespace BusBookingSystem.Tests.UnitTests
{
    public class RouteServiceTest
    {
        private RouteService CreateServiceWithContext(out ApplicationDbContext context)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            var repository = new RouteRepository(context);
            return new RouteService(repository);
        }

        [Fact]
        public void CanAddRoute()
        {
            var service = CreateServiceWithContext(out var context);

            var createRouteDto = new CreateRouteDto
            {
                Source = "CityA",
                Destination = "CityB",
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now.AddHours(2)
            };

            var route = service.AddRoute(createRouteDto);

            Assert.Equal(1, context.Routes.Count());
            Assert.Equal("CityA", route.Source);
        }

        [Fact]
        public void CanRetrieveRouteById()
        {
            var service = CreateServiceWithContext(out var context);

            var route = new Route
            {
                Source = "CityA",
                Destination = "CityB",
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now.AddHours(2)
            };

            context.Routes.Add(route);
            context.SaveChanges();

            var result = service.GetRouteById(route.RouteId);

            Assert.NotNull(result);
            Assert.Equal(route.RouteId, result.RouteId);
        }

        [Fact]
        public void CanUpdateRoute()
        {
            var service = CreateServiceWithContext(out var context);

            var route = new Route
            {
                Source = "CityA",
                Destination = "CityB",
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now.AddHours(2)
            };

            context.Routes.Add(route);
            context.SaveChanges();

            var updateRouteDto = new UpdateRouteDto
            {
                Source = "CityC",
                Destination = "CityD",
                DepartureTime = DateTime.Now.AddHours(1),
                ArrivalTime = DateTime.Now.AddHours(3)
            };

            service.UpdateRoute(route.RouteId, updateRouteDto);

            var updatedRoute = context.Routes.Find(route.RouteId);

            Assert.Equal("CityC", updatedRoute.Source);
            Assert.Equal("CityD", updatedRoute.Destination);
        }

        [Fact]
        public void CanDeleteRoute()
        {
            var service = CreateServiceWithContext(out var context);

            var route = new Route
            {
                Source = "CityA",
                Destination = "CityB",
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now.AddHours(2)
            };

            context.Routes.Add(route);
            context.SaveChanges();

            service.DeleteRoute(route.RouteId);

            var deletedRoute = context.Routes.Find(route.RouteId);
            Assert.Null(deletedRoute);
        }
    }
}
