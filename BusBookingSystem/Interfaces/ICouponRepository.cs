﻿using System.Collections.Generic;
using BusBookingSystem.Models;

namespace BusBookingSystem.Interfaces
{
    public interface ICouponRepository
    {
        Coupon GetCouponById(int couponId);
        IEnumerable<Coupon> GetAllCoupons();
        Coupon GetCouponByCode(string code);

        void AddCoupon(Coupon coupon);
        void UpdateCoupon(Coupon coupon);
        void DeleteCoupon(int couponId);
    }
}