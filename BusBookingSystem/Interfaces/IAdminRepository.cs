using System.Collections.Generic;
using BusBookingSystem.Models;

namespace BusBookingSystem.Interfaces
{
    public interface IAdminRepository
    {
        Admin GetAdminById(int adminId);
        IEnumerable<Admin> GetAllAdmins();
        void AddAdmin(Admin admin);
        void UpdateAdmin(Admin admin);
        void DeleteAdmin(int adminId);
        Admin GetAdminByEmail(string email);
    }
}