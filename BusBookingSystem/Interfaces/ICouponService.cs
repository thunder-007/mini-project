using BusBookingSystem.Dtos;
using System.Collections.Generic;

namespace BusBookingSystem.Interfaces
{
    public interface ICouponService
    {
        CouponDto GetCouponById(int couponId);
        IEnumerable<CouponDto> GetAllCoupons();
        void AddCoupon(CreateCouponDto createCouponDto);
        void UpdateCoupon(CouponDto couponDto);
        void DeleteCoupon(int couponId);
    }
}