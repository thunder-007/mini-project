using System;
using BusBookingSystem.Interfaces;
using BusBookingSystem.Models;

public class BookingService
{
    private readonly IBusRepository _busRepository;
    private readonly ICouponRepository _couponRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IPaymentRepository _paymentRepository;

    public BookingService(
        IBusRepository busRepository,
        ICouponRepository couponRepository,
        IBookingRepository bookingRepository,
        IPaymentRepository paymentRepository)
    {
        _busRepository = busRepository;
        _couponRepository = couponRepository;
        _bookingRepository = bookingRepository;
        _paymentRepository = paymentRepository;
    }

    public Booking CreateBooking(int userId, CreateBookingDto dto)
    {
        var bus = _busRepository.GetBusById(dto.BusId);
        if (bus == null)
        {
            throw new Exception("Bus not found");
        }

        var route = bus.Route;
        if (route.DepartureTime < DateTime.Now)
        {
            throw new Exception("Bus has already departed");
        }
        if (dto.BookingDate >= route.DepartureTime)
        {
            throw new Exception("Booking date must be before the departure date");
        }

        var currentBookingsCount = _bookingRepository.GetBookingsCountByBusId(dto.BusId);
        if (currentBookingsCount >= bus.Capacity)
        {
            throw new Exception("Bus is fully booked");
        }
        var existingBooking = _bookingRepository.GetBookingByBusIdAndSeatNumber(dto.BusId, dto.SeatNumber);
        if (existingBooking != null)
        {
            throw new Exception("Seat number is already booked");
        }
        decimal discountAmount = 0;


        Coupon coupon = null;
        if (!string.IsNullOrEmpty(dto.CouponCode))
        {
            coupon = _couponRepository.GetCouponByCode(dto.CouponCode);
            if (coupon == null || coupon.ExpiryDate < DateTime.Now || coupon.RedemptionLimit <= 0)
            {
                throw new Exception("Invalid or expired coupon");
            }
            discountAmount = dto.Amount * (coupon.DiscountPercentage / 100m);

            coupon.RedemptionLimit--;
            _couponRepository.UpdateCoupon(coupon);
        }
        var finalAmount = dto.Amount - discountAmount;


        var booking = new Booking
        {
            UserId = userId,
            BusId = dto.BusId,
            BookingDate = dto.BookingDate,
            SeatNumber = dto.SeatNumber
        };
        _bookingRepository.CreateBooking(booking);

        var payment = new Payment
        {
            BookingId = booking.BookingId,
            Amount = finalAmount,
            PaymentDate = DateTime.Now,
            PaymentMethod = dto.PaymentMethod,
            Status = "Completed"
        };
        _paymentRepository.CreatePayment(payment);

        return booking;
    }
}
