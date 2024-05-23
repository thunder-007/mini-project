using System;
using System.ComponentModel.DataAnnotations;

namespace BusBookingSystem.Models;

public class Coupon
{
    [Key] public int CouponId { get; set; }

    [Required] [MaxLength(20)] public string Code { get; set; }

    [Required] public int DiscountPercentage { get; set; }

    [Required] public DateTime ExpiryDate { get; set; }

    [Required] public int RedemptionLimit { get; set; }

    [Required] public int BusId { get; set; }

    public Bus Bus { get; set; }
}
