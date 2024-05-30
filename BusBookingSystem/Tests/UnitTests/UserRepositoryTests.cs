using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BusBookingSystem.Models;
using BusBookingSystem.Repositories;
using BusBookingSystem.Tests.TestUtilities;

namespace BusBookingSystem.Tests
{
    public class UserRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly UserRepository _userRepository;

        public UserRepositoryTests()
        {
            _context = DbContextFactory.Create();
            _userRepository = new UserRepository(_context);
        }

        [Fact]
        public void GetUserById_ShouldReturnUser_WhenUserExists()
        {
            
            var userId = 1;
            var user = new User
            {
                UserId = userId,
                UserName = "TestUser",
                Email = "test@example.com",
                PasswordHash = "passwordhash",
                Role = "User"
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            
            var result = _userRepository.GetUserById(userId);

            
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
        }

        [Fact]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            
            var users = new[]
            {
                new User
                {
                    UserId = 1,
                    UserName = "TestUser1",
                    Email = "test1@example.com",
                    PasswordHash = "passwordhash1",
                    Role = "User"
                },
                new User
                {
                    UserId = 2,
                    UserName = "TestUser2",
                    Email = "test2@example.com",
                    PasswordHash = "passwordhash2",
                    Role = "User"
                }
            };
            _context.Users.AddRange(users);
            _context.SaveChanges();

            
            var result = _userRepository.GetAllUsers();

            
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void AddUser_ShouldAddUser()
        {
            
            var user = new User
            {
                UserId = 1,
                UserName = "TestUser",
                Email = "test@example.com",
                PasswordHash = "passwordhash",
                Role = "User"
            };

            
            _userRepository.AddUser(user);
            var result = _context.Users.Find(user.UserId);

            
            Assert.NotNull(result);
            Assert.Equal(user.UserId, result.UserId);
        }

        [Fact]
        public void UpdateUser_ShouldUpdateUser()
        {
            
            var user = new User
            {
                UserId = 1,
                UserName = "TestUser",
                Email = "test@example.com",
                PasswordHash = "passwordhash",
                Role = "User"
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            user.UserName = "UpdatedUser";

            
            _userRepository.UpdateUser(user);
            var result = _context.Users.Find(user.UserId);

            
            Assert.NotNull(result);
            Assert.Equal("UpdatedUser", result.UserName);
        }

        [Fact]
        public void DeleteUser_ShouldRemoveUser()
        {
            
            var user = new User
            {
                UserId = 1,
                UserName = "TestUser",
                Email = "test@example.com",
                PasswordHash = "passwordhash",
                Role = "User"
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            
            _userRepository.DeleteUser(user.UserId);
            var result = _context.Users.Find(user.UserId);

            
            Assert.Null(result);
        }

        [Fact]
        public void GetUserByEmail_ShouldReturnUser_WhenEmailExists()
        {
            
            var email = "test@example.com";
            var user = new User
            {
                UserId = 1,
                UserName = "TestUser",
                Email = email,
                PasswordHash = "passwordhash",
                Role = "User"
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            
            var result = _userRepository.GetUserByEmail(email);

            
            Assert.NotNull(result);
            Assert.Equal(email, result.Email);
        }
    }
}
