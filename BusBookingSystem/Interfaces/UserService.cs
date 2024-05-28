using System.Collections.Generic;
using BusBookingSystem.Dtos;
using BusBookingSystem.Models;

namespace BusBookingSystem.Interfaces
{
    public interface IUserService
    {
        User GetUserById(int userId);
        IEnumerable<User> GetAllUsers();
        void RegisterUser(RegisterUserDto user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
        bool ValidateUser(string email, string password);
        User GetUserByEmail(string email);
    }
}