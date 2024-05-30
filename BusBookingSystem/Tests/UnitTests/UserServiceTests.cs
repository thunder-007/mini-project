using Xunit;
using Moq;
using System.Collections.Generic;
using BusBookingSystem.Models;
using BusBookingSystem.Interfaces;
using BusBookingSystem.Services;
using BusBookingSystem.Dtos;
using BusBookingSystem.Utils;

namespace BusBookingSystem.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserService _userService;
        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }
        [Fact]
        public void GetUserById_ShouldReturnUser_WhenUserExists()
        {
            var userId = 1;
            var user = new User { UserId = userId, UserName = "TestUser" };
            _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns(user);
            var result = _userService.GetUserById(userId);
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
        }

        [Fact]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            var users = new List<User>
            {
                new User { UserId = 1, UserName = "TestUser1" },
                new User { UserId = 2, UserName = "TestUser2" }
            };
            _userRepositoryMock.Setup(repo => repo.GetAllUsers()).Returns(users);
            var result = _userService.GetAllUsers();
            Assert.NotNull(result);
            Assert.Equal(2, ((List<User>)result).Count);
        }

        [Fact]
        public void RegisterUser_ShouldAddUser()
        {
            
            var registerUserDto = new RegisterUserDto
            {
                UserName = "TestUser",
                Email = "test@example.com",
                Password = "Password123"
            };

            
            _userService.RegisterUser(registerUserDto);
            _userRepositoryMock.Verify(repo => repo.AddUser(It.Is<User>(u =>
                u.UserName == registerUserDto.UserName &&
                u.Email == registerUserDto.Email &&
                u.Role == "User"
            )), Times.Once);
        }

        [Fact]
        public void UpdateUser_ShouldUpdateUser()
        {
            
            var user = new User
            {
                UserId = 1,
                UserName = "TestUser",
                PasswordHash = PasswordHelper.HashPassword("OldPassword"),
                Email = "test@example.com",
                Role = "User"
            };
            user.PasswordHash = PasswordHelper.HashPassword("NewPassword");
            _userService.UpdateUser(user);
            _userRepositoryMock.Verify(repo => repo.UpdateUser(It.Is<User>(u =>
                u.UserId == user.UserId &&
                u.PasswordHash == user.PasswordHash
            )), Times.Once);
        }

        [Fact]
        public void DeleteUser_ShouldRemoveUser()
        {
            
            var userId = 1;
            _userService.DeleteUser(userId);
            _userRepositoryMock.Verify(repo => repo.DeleteUser(userId), Times.Once);
        }
        

        [Fact]
        public void ValidateUser_ShouldReturnFalse_WhenCredentialsAreInvalid()
        {
            
            var email = "test@example.com";
            var password = "InvalidPassword";
            var user = new User
            {
                Email = email,
                PasswordHash = PasswordHelper.HashPassword("Password123")
            };
            _userRepositoryMock.Setup(repo => repo.GetUserByEmail(email)).Returns(user);

            
            var result = _userService.ValidateUser(email, password);

            
            Assert.False(result);
        }
    }
}
