using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using BusBookingSystem.Controllers;
using BusBookingSystem.Dtos;
using BusBookingSystem.Interfaces;
using System.Collections.Generic;
using System;

namespace BusBookingSystem.Tests
{
    public class RouteControllerTests
    {
        private readonly Mock<IRouteService> _routeServiceMock;
        private readonly RouteController _routeController;

        public RouteControllerTests()
        {
            _routeServiceMock = new Mock<IRouteService>();
            _routeController = new RouteController(_routeServiceMock.Object);
        }

        [Fact]
        public void GetRouteById_ShouldReturnOkResult_WhenRouteExists()
        {
            // Arrange
            var routeId = 1;
            var route = new RouteDto 
            { 
                RouteId = routeId, 
                Source = "A", 
                Destination = "B", 
                DepartureTime = DateTime.Now.AddHours(1), 
                ArrivalTime = DateTime.Now.AddHours(5) 
            };
            _routeServiceMock.Setup(service => service.GetRouteById(routeId)).Returns(route);

            // Act
            var result = _routeController.GetRouteById(routeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<RouteDto>(okResult.Value);
            Assert.Equal(routeId, returnValue.RouteId);
            Assert.Equal("A", returnValue.Source);
            Assert.Equal("B", returnValue.Destination);
        }

        [Fact]
        public void GetRouteById_ShouldReturnNotFound_WhenRouteDoesNotExist()
        {
            // Arrange
            var routeId = 1;
            _routeServiceMock.Setup(service => service.GetRouteById(routeId)).Returns((RouteDto)null);

            // Act
            var result = _routeController.GetRouteById(routeId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetAllRoutes_ShouldReturnOkResult_WithAllRoutes()
        {
            // Arrange
            var routes = new List<RouteDto>
            {
                new RouteDto 
                { 
                    RouteId = 1, 
                    Source = "A", 
                    Destination = "B", 
                    DepartureTime = DateTime.Now.AddHours(1), 
                    ArrivalTime = DateTime.Now.AddHours(5) 
                },
                new RouteDto 
                { 
                    RouteId = 2, 
                    Source = "B", 
                    Destination = "C", 
                    DepartureTime = DateTime.Now.AddHours(2), 
                    ArrivalTime = DateTime.Now.AddHours(6) 
                }
            };
            _routeServiceMock.Setup(service => service.GetAllRoutes()).Returns(routes);

            // Act
            var result = _routeController.GetAllRoutes();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<RouteDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        

        [Fact]
        public void UpdateRoute_ShouldReturnNoContentResult()
        {
            // Arrange
            var routeId = 1;
            var updateRouteDto = new UpdateRouteDto 
            { 
                Source = "A", 
                Destination = "C", 
                DepartureTime = DateTime.Now.AddHours(2), 
                ArrivalTime = DateTime.Now.AddHours(6) 
            };

            // Act
            var result = _routeController.UpdateRoute(routeId, updateRouteDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _routeServiceMock.Verify(service => service.UpdateRoute(routeId, updateRouteDto), Times.Once);
        }

        [Fact]
        public void DeleteRoute_ShouldReturnNoContentResult()
        {
            // Arrange
            var routeId = 1;

            // Act
            var result = _routeController.DeleteRoute(routeId);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _routeServiceMock.Verify(service => service.DeleteRoute(routeId), Times.Once);
        }
    }
}
