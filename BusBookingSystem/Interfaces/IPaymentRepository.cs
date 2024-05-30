using System.Collections.Generic;
using BusBookingSystem.Models;

namespace BusBookingSystem.Interfaces
{
    public interface IPaymentRepository
    {
        void CreatePayment(Payment payment);
        Payment GetPaymentById(int paymentId);
        IEnumerable<Payment> GetAllPayments();
        void AddPayment(Payment payment);
        void UpdatePayment(Payment payment);
        void DeletePayment(int paymentId);
    }
}