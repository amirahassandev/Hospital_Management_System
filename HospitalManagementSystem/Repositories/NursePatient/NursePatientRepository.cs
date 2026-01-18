using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.NursePatient;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repositories
{
    public class NursePatientRepository : INursePatientRepository
    {
        private readonly HospitalDbContext _db;
        public NursePatientRepository(HospitalDbContext _db) 
        {
            this._db = _db;
        }

        async Task<IEnumerable<NursePatient>> INursePatientRepository.GetAllNursePatients()
        {
            return await _db.NursePatients
                .Include(np => np.Nurse)
                    .ThenInclude(n => n.User)
                .Include(np => np.Patient)
                    .ThenInclude(p => p.User)
                .ToListAsync();
        }

        async Task<bool> INursePatientRepository.IsNursePatientExist(int nurseId, int patientId)
        {
            var nursePatient = await _db.NursePatients.Where(np => (np.NurseId == nurseId) && (np.PatientId == patientId)).FirstOrDefaultAsync();
            if (nursePatient != null) return true;
            return false;
        }

        async Task<NursePatient?> INursePatientRepository.GetNursePatient(int nurseId, int patientId)
        {
            var nursePatient = await _db.NursePatients
                .Where(np => (np.NurseId == nurseId) && (np.PatientId == patientId))
                .Include(np => np.Nurse)
                .   ThenInclude(n => n.User)
                .Include(np => np.Patient)
                    .ThenInclude(p => p.User)
                .FirstOrDefaultAsync();
            return nursePatient;
        }

        async Task<NursePatient?> INursePatientRepository.AssignNurseToPatient(NursePatient nursePatient)
        {   
            await _db.NursePatients.AddAsync(nursePatient);
            var isAssigned = await _db.SaveChangesAsync();
            if (isAssigned == 0) return null;
            return await ((INursePatientRepository)this).GetNursePatient(nursePatient.NurseId, nursePatient.PatientId);
        }

        async Task<bool> INursePatientRepository.UpdateNursePatient(NursePatient nursePatient)
        {
            _db.NursePatients.Update(nursePatient);
            var isUpdated = await _db.SaveChangesAsync();
            return isUpdated > 0;
        }

        async Task<bool> INursePatientRepository.RemoveNursePatient(NursePatient nursePatient)
        {
            _db.NursePatients.Remove(nursePatient);
            var isRemoved = await _db.SaveChangesAsync();
            return isRemoved > 0;
        }
        //async Task<IEnumerable<NursePatient?>> INursePatientRepository.GetAssignmentsForNurse(int nurseId)
        //{

        //}
        async Task<IEnumerable<NursePatient?>> INursePatientRepository.GetAssignmentsForPatient(int patientId)
        {
            return await _db.NursePatients.Where(np => np.PatientId == patientId).ToListAsync();
        }
    }
}
