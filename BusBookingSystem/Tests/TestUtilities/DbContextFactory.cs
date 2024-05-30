using Microsoft.EntityFrameworkCore;
using BusBookingSystem.Models;

namespace BusBookingSystem.Tests.TestUtilities
{
    public static class DbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            return context;
        }
    }
}