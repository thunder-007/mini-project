using System;
using System.ComponentModel.DataAnnotations;

public class CreateBookingDto
{

    [Required]
    public int BusId { get; set; }

    [Required]
    public DateTime BookingDate { get; set; }

    [Required]
    public int SeatNumber { get; set; }

    public string CouponCode { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    [MaxLength(20)]
    public string PaymentMethod { get; set; }
}