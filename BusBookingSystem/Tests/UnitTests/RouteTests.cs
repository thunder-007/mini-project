using System;
using System.Linq;
using Xunit;
using BusBookingSystem.Models;
using BusBookingSystem.Tests.TestUtilities;
using Microsoft.EntityFrameworkCore;
using Route = BusBookingSystem.Models.Route;

namespace BusBookingSystem.Tests.UnitTests
{
    public class RouteTests
    {
        [Fact]
        public void CanAddRoute()
        {
            using (var context = DbContextFactory.Create())
            {
                var route = new Route
                {
                    Source = "CityA",
                    Destination = "CityB",
                    DepartureTime = DateTime.Now,
                    ArrivalTime = DateTime.Now.AddHours(2)
                };

                context.Routes.Add(route);
                context.SaveChanges();

                Assert.Equal(1, context.Routes.Count());
                Assert.Equal("CityA", context.Routes.First().Source);
            }
        }

        [Fact]
        public void CanRetrieveRouteBuses()
        {
            using (var context = DbContextFactory.Create())
            {
                var route = new Route
                {
                    Source = "CityA",
                    Destination = "CityB",
                    DepartureTime = DateTime.Now,
                    ArrivalTime = DateTime.Now.AddHours(2)
                };

                context.Routes.Add(route);
                context.SaveChanges();

                var bus = new Bus
                {
                    BusNumber = "BUS123",
                    Capacity = 50,
                    RouteId = route.RouteId,
                    Route = route
                };

                context.Buses.Add(bus);
                context.SaveChanges();

                var retrievedRoute = context.Routes.Include(r => r.Buses).First();
                Assert.Single(retrievedRoute.Buses);
                Assert.Equal("BUS123", retrievedRoute.Buses.First().BusNumber);
            }
        }
    }
}