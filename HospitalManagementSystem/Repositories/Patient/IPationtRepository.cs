using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.Patient;

namespace HospitalManagementSystem.Repositories
{
    public interface IPationtRepository
    {
        Task<IEnumerable<Patient>> GetAll();
        // Returns all patients (active and inactive) sorted ascending by default (e.g., by Name or Id).

        Task<IEnumerable<Patient>> GetAllDeactive();
        // Returns all deactivated (inactive) patients sorted ascending.

        Task<Patient?> GetPatient(int id);
        // Returns a single patient by their ID.

        Task<bool> DeletePatient(int id);
        // Soft-deletes (deactivates) a patient by ID. Marks patient as inactive.

        Task<bool> UpdatePatient(Patient patient);
        // Updates an existing patient's details in the database.

        Task<bool> AddPatient(Patient patient);
        // Adds a new patient to the database.

        Task<IEnumerable<Patient>> SearchByBloodType(string bloodType);
        // Returns a list of patients matching the given blood type.

        Task<IEnumerable<Patient>> SearchByName(string name);
        // Returns patients whose names match (partial or full) the search string.

        Task<IEnumerable<Patient>> SearchByGender(string gender);
        // Returns patients filtered by gender.

        // Task<IEnumerable<Patient>> SearchByMedicalCondition(string condition);  
        // Optional: Returns patients with a specific medical condition (if tracked).

        Task<string?> GetBloodType(int id);
        // Returns a blood type string (useful for validation or lookup).

        Task<bool> ReactivatePatient(int id);
        // Reactivates a previously deactivated patient.
        Task<bool> DeactivatePatient(int id);
        // Deactivates a previously reactivated patient.

        Task<bool> IsActiveAsync(int id);
        // Checks if a patient is currently active (not deactivated).

        Task<int> Count();
        // Returns the total number of patients in the database (active + inactive).
            
    }
}
