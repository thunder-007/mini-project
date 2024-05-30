using System;
using System.Linq;
using Xunit;
using BusBookingSystem.Models;
using BusBookingSystem.Tests.TestUtilities;
using Microsoft.EntityFrameworkCore;

namespace BusBookingSystem.Tests.UnitTests
{
    public class UserTests
    {
        [Fact]
        public void CanAddUser()
        {
            using (var context = DbContextFactory.Create())
            {
                var user = new User
                {
                    UserName = "testuser",
                    PasswordHash = "hashedpassword",
                    Email = "test@example.com",
                    Role = "User"
                };

                context.Users.Add(user);
                context.SaveChanges();

                Assert.Equal(1, context.Users.Count());
                Assert.Equal("testuser", context.Users.First().UserName);
            }
        }

        [Fact]
        public void CanRetrieveUserBookings()
        {
            using (var context = DbContextFactory.Create())
            {
                var user = new User
                {
                    UserName = "testuser",
                    PasswordHash = "hashedpassword",
                    Email = "test@example.com",
                    Role = "User",
                    Bookings = new System.Collections.Generic.List<Booking>
                    {
                        new Booking {}
                    }
                };

                context.Users.Add(user);
                context.SaveChanges();

                var retrievedUser = context.Users.Include(u => u.Bookings).First();
                Assert.Single(retrievedUser.Bookings);
            }
        }
    }
}