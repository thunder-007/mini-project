using BusBookingSystem.Dtos;
using BusBookingSystem.Interfaces;
using BusBookingSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace BusBookingSystem.Services
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepository _couponRepository;

        public CouponService(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public CouponDto GetCouponById(int couponId)
        {
            var coupon = _couponRepository.GetCouponById(couponId);
            return coupon == null ? null : MapToDto(coupon);
        }

        public IEnumerable<CouponDto> GetAllCoupons()
        {
            var coupons = _couponRepository.GetAllCoupons();
            return coupons.Select(c => MapToDto(c)).ToList();
        }

        public void AddCoupon(CreateCouponDto createCouponDto)
        {
            var coupon = new Coupon
            {
                Code = createCouponDto.Code,
                DiscountPercentage = createCouponDto.DiscountPercentage,
                ExpiryDate = createCouponDto.ExpiryDate,
                RedemptionLimit = createCouponDto.RedemptionLimit,
                BusId = createCouponDto.BusId
            };

            _couponRepository.AddCoupon(coupon);
        }

        public void UpdateCoupon(CouponDto couponDto)
        {
            var coupon = _couponRepository.GetCouponById(couponDto.CouponId);
            if (coupon != null)
            {
                coupon.Code = couponDto.Code;
                coupon.DiscountPercentage = couponDto.DiscountPercentage;
                coupon.ExpiryDate = couponDto.ExpiryDate;
                coupon.RedemptionLimit = couponDto.RedemptionLimit;
                coupon.BusId = couponDto.BusId;

                _couponRepository.UpdateCoupon(coupon);
            }
        }

        public void DeleteCoupon(int couponId)
        {
            _couponRepository.DeleteCoupon(couponId);
        }

        private CouponDto MapToDto(Coupon coupon)
        {
            return new CouponDto
            {
                CouponId = coupon.CouponId,
                Code = coupon.Code,
                DiscountPercentage = coupon.DiscountPercentage,
                ExpiryDate = coupon.ExpiryDate,
                RedemptionLimit = coupon.RedemptionLimit,
                BusId = coupon.BusId
            };
        }
    }
}
