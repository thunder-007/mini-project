using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BusBookingSystem.Controllers;
using BusBookingSystem.Interfaces;
using BusBookingSystem.Dtos;
using BusBookingSystem.Services;
using BusBookingSystem.Models;
using System;

namespace BusBookingSystem.Tests
{
    public class JwtControllerTests
    {
        private readonly Mock<IJwtService> _jwtServiceMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<ILogger<JwtController>> _loggerMock;
        private readonly JwtController _jwtController;

        public JwtControllerTests()
        {
            _jwtServiceMock = new Mock<IJwtService>();
            _userServiceMock = new Mock<IUserService>();
            _loggerMock = new Mock<ILogger<JwtController>>();
            _jwtController = new JwtController(_jwtServiceMock.Object, _userServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void GenerateToken_ShouldReturnOkResult_WhenCredentialsAreValid()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@example.com", Password = "Password123" };
            var user = new User { Email = loginDto.Email, Role = "User" };
            var token = "GeneratedJwtToken";

            _jwtServiceMock.Setup(service => service.ValidateUser(loginDto.Email, loginDto.Password)).Returns(true);
            _userServiceMock.Setup(service => service.GetUserByEmail(loginDto.Email)).Returns(user);
            _jwtServiceMock.Setup(service => service.GenerateToken(loginDto.Email, user.Role)).Returns(token);

            // Act
            var result = _jwtController.GenerateToken(loginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<JwtGetDto>(okResult.Value);
            Assert.Equal(token, returnValue.Token);
        }

        [Fact]
        public void GenerateToken_ShouldReturnBadRequest_WhenCredentialsAreInvalid()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@example.com", Password = "InvalidPassword" };

            _jwtServiceMock.Setup(service => service.ValidateUser(loginDto.Email, loginDto.Password)).Returns(false);

            // Act
            var result = _jwtController.GenerateToken(loginDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var returnValue = Assert.IsType<ErrorResponseDto>(badRequestResult.Value);
            Assert.Equal("Invalid username or password.", returnValue.Message);
        }

        [Fact]
        public void GenerateToken_ShouldReturnBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@example.com", Password = "Password123" };

            _jwtServiceMock.Setup(service => service.ValidateUser(loginDto.Email, loginDto.Password)).Throws(new Exception("Unexpected error"));

            // Act
            var result = _jwtController.GenerateToken(loginDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var returnValue = Assert.IsType<ErrorResponseDto>(badRequestResult.Value);
            Assert.Equal("Unexpected error", returnValue.Message);
        }
    }
}
