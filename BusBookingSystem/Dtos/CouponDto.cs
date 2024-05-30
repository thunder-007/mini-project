namespace BusBookingSystem.Dtos
{
    public class CouponDto
    {
        public int CouponId { get; set; }
        public string Code { get; set; }
        public int DiscountPercentage { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int RedemptionLimit { get; set; }
        public int BusId { get; set; }
    }

    public class CreateCouponDto
    {
        public string Code { get; set; }
        public int DiscountPercentage { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int RedemptionLimit { get; set; }
        public int BusId { get; set; }
    }
}