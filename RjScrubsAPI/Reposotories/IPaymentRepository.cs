using RjScrubs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RjScrubs.Repositories
{
    public interface IPaymentRepository
    {
        Task AddPaymentAsync(Payment payment);
        Task<Payment> GetPaymentByIdAsync(int id);
        Task UpdatePaymentAsync(Payment payment);
        Task DeletePaymentAsync(int id);
        Task<IEnumerable<Payment>> GetPaymentsByBookingIdAsync(int bookingId);
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
    }
}
