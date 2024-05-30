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

    public void CreateBooking(int userId, CreateBookingDto dto)
    {
        var bus = _busRepository.GetBusById(dto.BusId);
        if (bus == null)
        {
            throw new Exception("Bus not found");
        }
        var currentBookingsCount = _bookingRepository.GetBookingsCountByBusId(dto.BusId);
        if (currentBookingsCount >= bus.Capacity)
        {
            throw new Exception("Bus is fully booked");
        }


        
        Coupon coupon = null;
        if (!string.IsNullOrEmpty(dto.CouponCode))
        {
            coupon = _couponRepository.GetCouponByCode(dto.CouponCode);
            if (coupon == null || coupon.ExpiryDate < DateTime.Now || coupon.RedemptionLimit <= 0)
            {
                throw new Exception("Invalid or expired coupon");
            }
            coupon.RedemptionLimit--;
            _couponRepository.UpdateCoupon(coupon);
        }
        
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
            Amount = dto.Amount,
            PaymentDate = DateTime.Now,
            PaymentMethod = dto.PaymentMethod,
            Status = "Completed"
        };
        _paymentRepository.CreatePayment(payment);
    }
}
