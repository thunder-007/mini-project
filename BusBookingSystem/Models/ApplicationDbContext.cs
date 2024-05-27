using Microsoft.EntityFrameworkCore;

using System;

namespace BusBookingSystem.Models;
public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Bus> Buses { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Coupon>  Coupons { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=BusBooking;User Id=sa;Password=Password!123;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
    }
}

