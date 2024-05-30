using System;
using System.Linq;
using Xunit;
using BusBookingSystem.Models;
using BusBookingSystem.Tests.TestUtilities;
using Microsoft.EntityFrameworkCore;
using BusBookingSystem.Models;
using Route = BusBookingSystem.Models.Route;

namespace BusBookingSystem.Tests.UnitTests
{
    public class RouteRepositoryTest
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
        public void CanRetrieveRouteById()
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

                var retrievedRoute = context.Routes.Find(route.RouteId);
                Assert.NotNull(retrievedRoute);
                Assert.Equal(route.RouteId, retrievedRoute.RouteId);
            }
        }

        [Fact]
        public void CanUpdateRoute()
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

                route.Source = "CityC";
                context.Routes.Update(route);
                context.SaveChanges();

                var updatedRoute = context.Routes.Find(route.RouteId);
                Assert.Equal("CityC", updatedRoute.Source);
            }
        }

        [Fact]
        public void CanDeleteRoute()
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

                context.Routes.Remove(route);
                context.SaveChanges();

                var deletedRoute = context.Routes.Find(route.RouteId);
                Assert.Null(deletedRoute);
            }
        }
    }
}
