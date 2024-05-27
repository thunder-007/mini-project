using BusBookingSystem.Exceptions;
using BusBookingSystem.Models;
using System.Collections.Generic;
using System.Linq;
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

        public Admin GetAdminById(int adminId)
        {
            var admin = _context.Admins.Find(adminId);
            if (admin == null)
            {
                throw new NotFoundException($"Admin with ID {adminId} not found.");
            }
            return admin;
        }

        public IEnumerable<Admin> GetAllAdmins()
        {
            return _context.Admins.ToList();
        }

        public void AddAdmin(Admin admin)
        {
            if (_context.Admins.Any(a => a.Email == admin.Email))
            {
                throw new DuplicateRecordException($"An admin with the email {admin.Email} already exists.");
            }
            _context.Admins.Add(admin);
            _context.SaveChanges();
        }

        public void UpdateAdmin(Admin admin)
        {
            var existingAdmin = _context.Admins.Find(admin.AdminId);
            if (existingAdmin == null)
            {
                throw new NotFoundException($"Admin with ID {admin.AdminId} not found.");
            }

            existingAdmin.AdminName = admin.AdminName;
            existingAdmin.PasswordHash = admin.PasswordHash;
            existingAdmin.Email = admin.Email;

            _context.Admins.Update(existingAdmin);
            _context.SaveChanges();
        }

        public void DeleteAdmin(int adminId)
        {
            var admin = _context.Admins.Find(adminId);
            if (admin == null)
            {
                throw new NotFoundException($"Admin with ID {adminId} not found.");
            }

            _context.Admins.Remove(admin);
            _context.SaveChanges();
        }

        public Admin GetAdminByEmail(string email)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.Email == email);
            if (admin == null)
            {
                throw new NotFoundException($"Admin with email {email} not found.");
            }
            return admin;
        }
    }
}
