﻿using System.Collections.Generic;
using System.Linq;
using BusBookingSystem.Interfaces;
using BusBookingSystem.Models;

namespace BusBookingSystem.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ApplicationDbContext _context;

        public CouponRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Coupon GetCouponById(int couponId) => _context.Coupons.Find(couponId);

        public IEnumerable<Coupon> GetAllCoupons() => _context.Coupons.ToList();

        public void AddCoupon(Coupon coupon)
        {
            _context.Coupons.Add(coupon);
            _context.SaveChanges();
        }

        public void UpdateCoupon(Coupon coupon)
        {
            _context.Coupons.Update(coupon);
            _context.SaveChanges();
        }

        public void DeleteCoupon(int couponId)
        {
            var coupon = _context.Coupons.Find(couponId);
            if (coupon != null)
            {
                _context.Coupons.Remove(coupon);
                _context.SaveChanges();
            }
        }
    }
}