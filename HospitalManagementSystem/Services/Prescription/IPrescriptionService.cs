using HospitalManagementSystem.Dto.Prescription;

namespace HospitalManagementSystem.Services
{
    public interface IPrescriptionService
    {
        // Get all prescriptions
        Task<IEnumerable<ReadPrescriptionDto>> GetAllPrescriptionsAsync();

        // Get prescription by ID
        Task<ReadPrescriptionDto?> GetPrescriptionByIdAsync(int prescriptionId);

        // Create a new prescription
        Task<ReadPrescriptionDto?> CreatePrescriptionAsync(AddPrescriptionDto prescriptionDto);

        // Update a prescription
        Task<ReadPrescriptionDto?> UpdatePrescriptionAsync(int prescriptionId, UpdatePrescriptionDto prescriptionDto);

        // Delete a prescription
        //Task<bool> DeletePrescriptionAsync(int prescriptionId);

        // Get prescriptions by Medical Record ID
        Task<IEnumerable<ReadPrescriptionDto>> GetByMedicalIdAsync(int medicalId);

        // Get active prescriptions
        Task<IEnumerable<ReadPrescriptionDto>> GetActivePrescriptionsAsync();
    }
}
