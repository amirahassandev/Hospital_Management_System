

using HospitalManagementSystem.Dto.Billing;

namespace HospitalManagementSystem.Services
{
    public interface IBillingService
    {
        Task<bool> IsExist(int patientId, int paymentStatusId);
        Task<ReadBillingDto?> GetBillingAsync(int billingId);
        Task<IEnumerable<ReadBillingDto>> GetAllBillingsAsync();
        Task<ReadBillingDto?> CreateBillingAsync(AddBillingDto dto);
        Task<ReadBillingDto?> UpdateBillingAsync(int billingId, UpdateBillingDto dto);
        Task<bool> DeleteBillingAsync(int billingId);
    }
}
