using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.Patient;
using HospitalManagementSystem.Dto.User;

namespace HospitalManagementSystem.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<ReadPatient?>> GetAll();
        Task<ReadPatient?> GetPatient(int id);
        Task<IEnumerable<ReadPatient?>> GetAllDeactivatedPatientsAsync();
        Task<ReadPatient?> AddPatientAsync(AddPatientDto patient);
        Task<ReadPatient?> UpdatePatientAsync(int id, UpdatePatientDto dto);
        Task<ReadPatient?> DeletePatientAsync(int id);
        Task<ReadPatient?> ReactivatePatientAsync(int id);
        Task<ReadPatient?> DeactivatePatientAsync(int id);
        Task<string?> GetBloodTypeAsync(int id);
        Task<bool> IsActiveAsync(int id);
        Task<bool> IsAdultAsync(int id);
        Task<IEnumerable<ReadPatient?>> SearchByBloodTypeAsync(string bloodType);
        Task<IEnumerable<ReadPatient?>> SearchByAgeAsync(int minAge, int maxAge);
        Task<IEnumerable<ReadPatient?>> SearchByGenderAsync(string gender);
        Task<IEnumerable<ReadPatient?>> SearchByNameAsync(string name);
        Task<int> CountAsync();
    }
}
