using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repositories
{
    public class BillingRepository : IBillingRepository
    {
        private readonly HospitalDbContext _db;
        public BillingRepository(HospitalDbContext db)
        {
            _db = db;
        }

        async Task<bool> IBillingRepository.IsExist(int patientId, int paymentStatusId)
        {
            var billing = await _db.Billings
                .Where(b => (b.PatientId == patientId) && (b.PaymentStatusId == paymentStatusId))
                .FirstOrDefaultAsync();
            return (billing != null);
        }


        async Task<Billing?> IBillingRepository.AddAsync(Billing billing)
        {
            await _db.AddAsync(billing);
            var isAddet = await _db.SaveChangesAsync();
            if (isAddet == 0) return null;
            return await ((IBillingRepository)this).GetByIdAsync(billing.BillingId);
        }

        //async Task<bool> IBillingRepository.DeleteAsync(int billingId)
        //{
        //    //_db.Remove(billingId);

        //    var isDeleted = await _db.SaveChangesAsync();
        //    return isDeleted > 0;

        //}

        async Task<IEnumerable<Billing>?> IBillingRepository.GetAllAsync()
        {
            return await _db.Billings
                .Include(b => b.Patient)
                    .ThenInclude(p => p.User)
                .Include(b => b.PaymentStatus)
                .ToListAsync();
        }

        async Task<Billing?> IBillingRepository.GetByIdAsync(int billingId)
        {
            return await _db.Billings
                .Include(b => b.PaymentStatus)
                .Include(b => b.Patient)
                    .ThenInclude(p => p.User)
                .FirstOrDefaultAsync(b => b.BillingId == billingId);
        }

        async Task<bool> IBillingRepository.UpdateAsync(Billing billing)
        {
            _db.Billings.Update(billing);
            var isUpdated = await _db.SaveChangesAsync();
            return isUpdated > 0;
        }
    }
}
