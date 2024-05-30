using BusBookingSystem.Dtos;
using BusBookingSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace BusBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CouponDto>> GetAllCoupons()
        {
            var coupons = _couponService.GetAllCoupons();
            return Ok(coupons);
        }

        [HttpGet("{id}")]
        public ActionResult<CouponDto> GetCouponById(int id)
        {
            var coupon = _couponService.GetCouponById(id);
            if (coupon == null)
            {
                return NotFound();
            }
            return Ok(coupon);
        }

        [HttpPost]
        public ActionResult AddCoupon(CreateCouponDto createCouponDto)
        {
            _couponService.AddCoupon(createCouponDto);
            var addedCoupon = _couponService.GetAllCoupons().FirstOrDefault(c => c.Code == createCouponDto.Code);
            return CreatedAtAction(nameof(GetCouponById), new { id = addedCoupon.CouponId }, addedCoupon);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCoupon(int id, CouponDto couponDto)
        {
            if (id != couponDto.CouponId)
            {
                return BadRequest();
            }
            _couponService.UpdateCoupon(couponDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCoupon(int id)
        {
            _couponService.DeleteCoupon(id);
            return NoContent();
        }
    }
}
