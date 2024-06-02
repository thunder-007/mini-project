using System;
using System.ComponentModel.DataAnnotations;

namespace BusBookingSystem.Models
{
    public class Cancellation
    {
        [Key]
        public int CancellationId { get; set; }

        [Required]
        public int BookingId { get; set; }

        [Required]
        public DateTime CancellationDate { get; set; }

        [MaxLength(500)]
        public string Reason { get; set; }

        public Booking Booking { get; set; }
    }
}