using System.Collections.Generic;
using BusBookingSystem.Models;
using BusBookingSystem.Interfaces;
using BusBookingSystem.Utils;

namespace BusBookingSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUserById(int userId) => _userRepository.GetUserById(userId);

        public IEnumerable<User> GetAllUsers() => _userRepository.GetAllUsers();

        public void RegisterUser(User user)
        {
            user.PasswordHash = PasswordHelper.HashPassword(user.PasswordHash);
            _userRepository.AddUser(user);
        }

        public void UpdateUser(User user)
        {
            user.PasswordHash = PasswordHelper.HashPassword(user.PasswordHash);
            _userRepository.UpdateUser(user);
        }

        public void DeleteUser(int userId) => _userRepository.DeleteUser(userId);

        public bool ValidateUser(string email, string password) => _userRepository.ValidateUser(email, password);

        public User GetUserByEmail(string email) => _userRepository.GetUserByEmail(email);
    }
}