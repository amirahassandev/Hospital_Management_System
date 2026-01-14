using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.Patient;
using HospitalManagementSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repositories
{
    public class PatientRepository : IPationtRepository
    {
        private readonly HospitalDbContext _db;
        private readonly IUserRepository _userRep;

        public PatientRepository(HospitalDbContext db, IUserRepository userRepo) 
        {
            _db = db;
            _userRep = userRepo;
        }

        // ################  GET   ################ //
        async Task<IEnumerable<Patient>> IPationtRepository.GetAll()
        {
            var patients = await _db.Patients
                .Include(p => p.User)
                .ThenInclude(u => u.Role)
                .ToListAsync();
            return patients;
        }
        async Task<Patient> IPationtRepository.GetPatient(int id)
        {
            var patient = await _db.Patients
                .Include(p => p.User)
                .ThenInclude(u => u.Role)
                .FirstOrDefaultAsync(p => p.PatientId == id);
            return patient;
        }
        async Task<IEnumerable<Patient>> IPationtRepository.GetAllDeactive()
        {
            IEnumerable<Patient> deactivePatients = await _db.Patients
                .Where(p => p.IsActive == false)
                .Include(p => p.User)
                .ThenInclude(u => u.Role)
                .ToListAsync();
            return deactivePatients;
        }
        async Task<string?> IPationtRepository.GetBloodType(int id)
        {
            var bloodType = await _db.Patients
                .Where(p => p.IsActive == true && p.PatientId == id)
                .Select(p => p.BloodType)
                .FirstOrDefaultAsync();
            return bloodType;
        }
        async Task<int> IPationtRepository.Count()
        {
            int count = await _db.Patients.CountAsync();
            return count;
        }


        // ################  ADD   ################ /
        async Task<bool> IPationtRepository.AddPatient(Patient patient)
        {
            if (patient == null) return false;
            await _db.Patients.AddAsync(patient);
            var isAdded = await _db.SaveChangesAsync();
            return isAdded > 0;
        }


        // ################  UPDATE   ################ /
        async Task<bool> IPationtRepository.UpdatePatient(Patient patient)
        {
            //var patient = await _db.Patients.FirstOrDefaultAsync(p => p.PatientId == id);
            _db.Patients.Update(patient);
            var isUpdated = await _db.SaveChangesAsync();
            return isUpdated > 1;
        }

        async Task<bool> IPationtRepository.ReactivatePatient(int id)
        {
            var patient = await _db.Patients.FirstOrDefaultAsync(p => p.PatientId == id);
            if (patient == null) return false;

            patient.IsActive = true;
            patient.User.IsActive = true;
            return await _db.SaveChangesAsync() > 0;
        }

        async Task<bool> IPationtRepository.DeactivatePatient(int id)
        {
            var patient = await _db.Patients.FirstOrDefaultAsync(p => p.PatientId == id);
            if (patient == null) return false;

            patient.IsActive = false;
            patient.User.IsActive = false;
            return await _db.SaveChangesAsync() > 0;
        }


        // ################  DELETE   ################ /
        async Task<bool> IPationtRepository.DeletePatient(int id)
        {
            var patient = await _db.Patients.FirstOrDefaultAsync(p => p.PatientId == id);
            return await ((IPationtRepository)this).DeactivatePatient(id);
        }

        // ################  IS ??   ################ /

        async Task<bool> IPationtRepository.IsActiveAsync(int id)
        {
            var patient = await _db.Patients.Where(p => p.IsActive).FirstOrDefaultAsync(p => p.PatientId == id);
            return patient != null;
        }



        // ################  SEARCH   ################ /
        async Task<IEnumerable<Patient>> IPationtRepository.SearchByBloodType(string bloodType)
        {
            if (string.IsNullOrWhiteSpace(bloodType))
                return new List<Patient>();

            bloodType = bloodType.Trim().ToUpper();

            var patients = await _db.Patients
                .Where(p => p.BloodType != null &&
                            EF.Functions.Like(p.BloodType, bloodType + "%"))
                .ToListAsync();

            return patients;
        }



        async Task<IEnumerable<Patient>> IPationtRepository.SearchByGender(string gender)
        {
            if (string.IsNullOrWhiteSpace(gender))
                return new List<Patient>();

            bool gen = gender.Trim().ToLower() == "female"; // true if female, false if male

            var patients = await _db.Patients
                .Include(p => p.User)
                .Where(p => p.User != null && p.User.Gender == gen)
                .ToListAsync();

            return patients;
        }

        async Task<IEnumerable<Patient>> IPationtRepository.SearchByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new List<Patient>();

            name = name.Trim();

            var patients = await _db.Patients
                .Include(p => p.User)
                .Where(p => p.User != null &&
                       (
                           p.User.FirstName.Trim() == name ||
                           (p.User.LastName != null && p.User.LastName.Trim() == name)
                       ))
                .ToListAsync();

            return patients;
        }
    }
}
