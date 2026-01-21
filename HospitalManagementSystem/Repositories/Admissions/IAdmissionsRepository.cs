using HospitalManagementSystem.Data.Models;

namespace HospitalManagementSystem.Repositories
{
    public interface IAdmissionRepository
    {
        Task<IEnumerable<Admission>> GetAllAsync();
        // Get all admissions

        Task<Admission?> GetByIdAsync(int admissionId);
        // Get admission by ID

        Task<Admission> AddAsync(Admission admission);
        // Add new admission

        Task<bool> UpdateAsync(Admission admission);
        // Update existing admission

        //Task<bool> Delete(Admission admission);
        // Delete admission

        Task<IEnumerable<Admission>> GetActiveAdmissionsAsync();
        // Returns all admissions where the patient is not discharged yet

        Task<bool> IsRoomAvailableAsync(string roomId);
        // Check if room is available

        Task<bool> IsRoomExistedAsync(string roomNumber);

        Task<bool> PatientHasActiveAdmissionAsync(int patientId);
        // Check if patient already admitted
        Task<IEnumerable<Admission>> GetByPatientIdAsync(int patientId);
        // Get admissions by patient ID

        Task<IEnumerable<Admission>> GetByRoomIdAsync(int roomId);
        // Get admissions by room ID

        Task<IEnumerable<Admission>> GetByDateAsync(DateOnly date);
        // Get admissions by date
        Task<bool> IsExistsAsync(int admissionId);
        // Check if admission exists
    }
}
