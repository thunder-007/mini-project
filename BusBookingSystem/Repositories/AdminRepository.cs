using System.Collections.Generic;
using System.Linq;
using BusBookingSystem.Models;
using BusBookingSystem.Interfaces;

namespace BusBookingSystem.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Admin GetAdminById(int adminId) => _context.Admins.Find(adminId);

        public IEnumerable<Admin> GetAllAdmins() => _context.Admins.ToList();

        public void AddAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            _context.SaveChanges();
        }

        public void UpdateAdmin(Admin admin)
        {
            _context.Admins.Update(admin);
            _context.SaveChanges();
        }

        public void DeleteAdmin(int adminId)
        {
            var admin = _context.Admins.Find(adminId);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
                _context.SaveChanges();
            }
        }

        public Admin GetAdminByEmail(string email) => _context.Admins.FirstOrDefault(a => a.Email == email);
    }
}