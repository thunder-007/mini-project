using System.Collections.Generic;
using System.Linq;
using BusBookingSystem.Models;
using BusBookingSystem.Interfaces;
using BusBookingSystem.Utils;

namespace BusBookingSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User GetUserById(int userId) => _context.Users.Find(userId);

        public IEnumerable<User> GetAllUsers() => _context.Users.ToList();

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public User GetUserByEmail(string email) => _context.Users.FirstOrDefault(u => u.Email == email);
        
        public bool ValidateUser(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
    
            return user != null && PasswordHelper.VerifyPassword(password, user.PasswordHash);
        }
    }
}