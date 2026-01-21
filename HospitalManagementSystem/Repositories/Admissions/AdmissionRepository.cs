using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.IsisMtt.X509;

namespace HospitalManagementSystem.Repositories.Admissions
{
    public class AdmissionRepository : IAdmissionRepository
    {
        private readonly HospitalDbContext _db;
        public AdmissionRepository(HospitalDbContext _db) 
        {
            this._db = _db;
        }

        async Task<Admission?> IAdmissionRepository.AddAsync(Admission admission)
        {
            await _db.Admissions.AddAsync(admission);
            var isAddet = await _db.SaveChangesAsync();
            if (isAddet == 0) return null;

            return await _db.Admissions
                .Include(a => a.Patient)
                    .ThenInclude(p => p.User)
                .Include(a => a.Room)
                    .ThenInclude(r => r.Department)
                .Include(a => a.Room)
                    .ThenInclude(r => r.RoomStatus)
                .FirstOrDefaultAsync(a => a.AdmissionsId == admission.AdmissionsId);
        }

        //async Task<bool> IAdmissionRepository.Delete(Admission admission)
        //{
        //     _db.Admissions.Remove(admission);
        //    var isDeleted = await _db.SaveChangesAsync();
        //    return isDeleted > 0;
        //}

        async Task<bool> IAdmissionRepository.IsExistsAsync(int admissionId)
        {
            var admision = await ((IAdmissionRepository)this).GetByIdAsync(admissionId);
            return (admision != null);
        }

        async Task<IEnumerable<Admission>> IAdmissionRepository.GetActiveAdmissionsAsync()
        {
            return await _db.Admissions
                .Include(a => a.Patient)
                    .ThenInclude(p => p.User)
                .Include(a => a.Room)
                    .ThenInclude(r => r.RoomStatus)
                .Include(a => a.Room)
                    .ThenInclude(r => r.Department)
                .Where(p => (p.DischargeDate.HasValue) && (p.DischargeDate.Value < DateOnly.FromDateTime(DateTime.Now)))
                .ToListAsync();
        }

        async Task<IEnumerable<Admission>> IAdmissionRepository.GetAllAsync()
        {
            return await _db.Admissions
                .Include(a => a.Patient)
                    .ThenInclude(p => p.User)
                .Include(a => a.Room)
                    .ThenInclude(r => r.RoomStatus)
                .Include(a => a.Room)
                    .ThenInclude(r => r.Department)
                .ToListAsync();
        }

        async Task<IEnumerable<Admission>> IAdmissionRepository.GetByDateAsync(DateOnly date)
        {
            return await _db.Admissions.Where(a => a.AdmissionDate == date).ToListAsync();
        }

        async Task<Admission?> IAdmissionRepository.GetByIdAsync(int admissionId)
        {
            return await _db.Admissions
                .Include(a => a.Patient)
                    .ThenInclude(p => p.User)
                .Include(a => a.Room)
                    .ThenInclude(r => r.RoomStatus)
                .Include(a => a.Room)
                    .ThenInclude(r => r.Department)
                .FirstOrDefaultAsync(a => a.AdmissionsId == admissionId);
        }

        async Task<IEnumerable<Admission>> IAdmissionRepository.GetByPatientIdAsync(int patientId)
        {
            return await _db.Admissions
                .Include(a => a.Patient)
                    .ThenInclude(p => p.User)
                .Include(a => a.Room)
                    .ThenInclude(r => r.RoomStatus)
                .Include(a => a.Room)
                    .ThenInclude(r => r.Department)
                .Where(a => a.PatientId == patientId)
                .ToListAsync();
        }

        async Task<IEnumerable<Admission>> IAdmissionRepository.GetByRoomIdAsync(int roomId)
        {
            return await _db.Admissions
                .Include(a => a.Patient)
                    .ThenInclude(p => p.User)
                .Include(a => a.Room)
                    .ThenInclude(r => r.RoomStatus)
                .Include(a => a.Room)
                    .ThenInclude(r => r.Department)
                .Where(r => r.RoomId == roomId)
                .ToListAsync();
        }

        async Task<bool> IAdmissionRepository.IsRoomAvailableAsync(string roomNumber)
        {
            var room = await _db.Rooms.Include(r => r.RoomStatus).FirstOrDefaultAsync(r => r.RoomNumber == roomNumber);

            if (room == null) return false;

            return room.RoomStatus.RoomStatusId == 1; // available
        }

        async Task<bool> IAdmissionRepository.IsRoomExistedAsync(string roomNumber)
        {
            var room = await _db.Rooms.FirstOrDefaultAsync(r => r.RoomNumber == roomNumber);
            return !(room == null);
        }

        async Task<bool> IAdmissionRepository.PatientHasActiveAdmissionAsync(int patientId)
        {
            return await _db.Admissions.AnyAsync(a => a.PatientId == patientId && a.Room.RoomStatusId == 1);
        }

        async Task<bool> IAdmissionRepository.UpdateAsync(Admission admission)
        {
            _db.Admissions.Update(admission);
            var isUpdated = await _db.SaveChangesAsync();
            return isUpdated > 0;

        }
    }
}
