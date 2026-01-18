using HospitalManagementSystem.Data.Models;

namespace HospitalManagementSystem.Repositories
{
    public interface IBillingRepository
    {
        Task<bool> IsExist(int patientId, int paymentStatusId);
        Task<Billing?> GetByIdAsync(int billingId);
        Task<IEnumerable<Billing>?> GetAllAsync();
        Task<Billing?> AddAsync(Billing billing);
        Task<bool> UpdateAsync(Billing billing);
        //Task<bool> DeleteAsync(int billingId);

    }
}
