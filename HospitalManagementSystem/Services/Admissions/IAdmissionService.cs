using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.Admission;
using HospitalManagementSystem.Repositories;

namespace HospitalManagementSystem.Services.Admissions
{
    public interface IAdmissionService
    {
        Task<IEnumerable<ReadAdmissionDto?>> GetAllAdmissionsAsync();
        // Get all admissions records

        Task<bool> IsRoomAvailableAsync(string roomNumber);
        Task<bool> IsRoomExistedAsync(string roomNumber);

        Task<ReadAdmissionDto?> GetAdmissionByIdAsync(int admissionId);
        //// Get a specific admission by ID

        Task<ReadAdmissionDto?> CreateAdmissionAsync(AddAdmissionDto admissionDto);
        //// Create a new admission

        Task<ReadAdmissionDto?> UpdateAdmissionAsync(int admissionId, UpdateAdmissionDto dto);
        // Update an existing admission

        //Task<ReadAdmissionDto?> DeleteAdmissionAsync(int admissionId);
        //// Delete an admission record

        //Task<bool> DischargePatientAsync(int admissionId);
        //// Discharge a patient (set discharge date to today)

        //Task<IEnumerable<ReadAdmissionDto>> GetActiveAdmissionsAsync();
        //// Get all currently admitted patients (not yet discharged)

        //Task<bool> IsRoomAvailableAsync(int roomNumber);
        //// Check if a room is available today

        //Task<bool> PatientHasActiveAdmissionAsync(int patientId);
        //// Check if the patient is currently admitted

        //Task<IEnumerable<ReadAdmissionDto>> GetAdmissionsByPatientIdAsync(int patientId);
        //// Get all admissions for a specific patient

        //Task<IEnumerable<ReadAdmissionDto>> GetAdmissionsByRoomIdAsync(int roomId);
        //// Get all admissions for a specific room

        //Task<IEnumerable<ReadAdmissionDto>> GetAdmissionsByDateAsync(DateOnly date);
        // Get admissions for a specific date
    }

}
