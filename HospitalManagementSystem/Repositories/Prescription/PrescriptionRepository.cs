
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly HospitalDbContext _db;
        public PrescriptionRepository(HospitalDbContext _db)
        {
            this._db = _db;
        }

        async Task<Prescription?> IPrescriptionRepository.AddAsync(Prescription prescription)
        {
            await _db.Prescriptions.AddAsync(prescription);
            var isAdded = await _db.SaveChangesAsync();
            if (isAdded == 0) return null;

            return await _db.Prescriptions
               .Include(p => p.Medical)
                   .ThenInclude(m => m.Patient)
                       .ThenInclude(p => p.User)
               .Include(p => p.Medical)
                   .ThenInclude(m => m.Doctor)
                       .ThenInclude(d => d.User)
               .FirstOrDefaultAsync(p => p.PrescriptionsId == prescription.PrescriptionsId);
        }

        //Task<bool> IPrescriptionRepository.DeleteAsync(int prescriptionId)
        //{
            
        //}

        async Task<IEnumerable<Prescription>> IPrescriptionRepository.GetActiveAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            return await _db.Prescriptions
                .Include(p => p.Medical)
                .Where(p => p.EndDate <= today)
                .ToListAsync();
        }

        async Task<IEnumerable<Prescription>> IPrescriptionRepository.GetAllAsync()
        {
            return await _db.Prescriptions
                .Include(p => p.Medical)
                    .ThenInclude(m => m.Patient)
                        .ThenInclude(p => p.User)
                .Include(p => p.Medical)
                    .ThenInclude(m => m.Doctor)
                        .ThenInclude(d => d.User)
                .ToListAsync();
        }

        async Task<Prescription?> IPrescriptionRepository.GetByIdAsync(int prescriptionId)
        {
            return await _db.Prescriptions
                .Include(p => p.Medical)
                    .ThenInclude(m => m.Patient)
                        .ThenInclude(p => p.User)
                .Include(p => p.Medical)
                    .ThenInclude(m => m.Doctor)
                        .ThenInclude(d => d.User)
                .FirstOrDefaultAsync(p => p.PrescriptionsId == prescriptionId);
        }

        async Task<IEnumerable<Prescription>> IPrescriptionRepository.GetByMedicalIdAsync(int medicalId)
        {
            return await _db.Prescriptions
                .Include(p => p.Medical)
                    .ThenInclude(m => m.Patient)
                        .ThenInclude(p => p.User)
                    .ThenInclude(m => m.Doctor)
                        .ThenInclude(d => d.User)
                .Where(p => p.MedicalId == medicalId)
                .ToListAsync();
        }

        async Task<bool> IPrescriptionRepository.IsExistsAsync(int prescriptionId)
        {
            return await _db.Prescriptions
                .Include(p => p.Medical)
                    .ThenInclude(m => m.Patient)
                        .ThenInclude(p => p.User)
                    .ThenInclude(m => m.Doctor)
                        .ThenInclude(d => d.User)
                .AnyAsync(p => p.PrescriptionsId == prescriptionId);
        }

        async Task<bool> IPrescriptionRepository.UpdateAsync(Prescription prescription)
        {
            _db.Prescriptions.Update(prescription);
            var isUpdated = await _db.SaveChangesAsync();
            return isUpdated > 0;
        }
    }
}
