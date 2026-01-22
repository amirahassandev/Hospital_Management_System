using HospitalManagementSystem.Data.Models;

namespace HospitalManagementSystem.Repositories
{
    public interface IPrescriptionRepository
    {
        // Get all prescriptions
        Task<IEnumerable<Prescription>> GetAllAsync();

        // Get prescription by ID
        Task<Prescription?> GetByIdAsync(int prescriptionId);

        // Add new prescription
        Task<Prescription?> AddAsync(Prescription prescription);

        // Update existing prescription
        Task<bool> UpdateAsync(Prescription prescription);

        // Delete a prescription
        //Task<bool> DeleteAsync(int prescriptionId);

        // Get prescriptions by Medical Record ID
        Task<IEnumerable<Prescription>> GetByMedicalIdAsync(int medicalId);

        // Check if prescription exists
        Task<bool> IsExistsAsync(int prescriptionId);

        // Get active prescriptions (EndDate null or in future)
        Task<IEnumerable<Prescription>> GetActiveAsync();
    }
}
