using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using BusBookingSystem.Models;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BookingsController : ControllerBase
{
    private readonly BookingService _bookingService;
    private readonly ApplicationDbContext _context;

    public BookingsController(BookingService bookingService, ApplicationDbContext context)
    {
        _bookingService = bookingService;
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateBooking([FromBody] CreateBookingDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var emailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (emailClaim == null)
            {
                return Unauthorized("Email claim not found in token.");
            }

            var email = emailClaim.Value;
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                return Unauthorized("User not found.");
            }

            _bookingService.CreateBooking(user.UserId, dto);
            return Ok("Booking created successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet]
    public IActionResult GetUserBookings()
    {
        try
        {
            var emailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (emailClaim == null)
            {
                return Unauthorized("Email claim not found in token.");
            }

            var email = emailClaim.Value;
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                return Unauthorized("User not found.");
            }

            var bookings = _context.Bookings
                .Where(b => b.UserId == user.UserId)
                .Select(b => new 
                {
                    b.BookingId,
                    b.BusId,
                    b.BookingDate,
                    b.SeatNumber,
                    BusDetails = new 
                    {
                        b.Bus.BusId,
                        b.Bus.BusNumber,
                        b.Bus.Capacity
                    },
                    PaymentDetails = new 
                    {
                        b.Payment.PaymentId,
                        b.Payment.Amount,
                        b.Payment.PaymentDate,
                        b.Payment.PaymentMethod,
                        b.Payment.Status
                    }
                })
                .ToList();


            return Ok(bookings);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpDelete("{bookingId}")]
    public IActionResult CancelBooking(int bookingId)
    {
        try
        {
            var emailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (emailClaim == null)
            {
                return Unauthorized("Email claim not found in token.");
            }

            var email = emailClaim.Value;
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                return Unauthorized("User not found.");
            }

            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == bookingId && b.UserId == user.UserId);
            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            // Record the cancellation details
            var cancellation = new Cancellation
            {
                BookingId = bookingId,
                CancellationDate = DateTime.UtcNow,
                Reason = "User requested cancellation"
            };
            _context.Cancellations.Add(cancellation);

            // Increase the bus capacity
            var bus = _context.Buses.FirstOrDefault(b => b.BusId == booking.BusId);
            if (bus != null)
            {
                bus.Capacity += 1;
                _context.Buses.Update(bus);
            }

            // Remove the booking
            _context.Bookings.Remove(booking);
            _context.SaveChanges();

            return Ok("Booking cancelled and bus capacity updated successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


}