using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.NursePatient;
using HospitalManagementSystem.Repositories;

namespace HospitalManagementSystem.Services
{
    public interface INursePatientService
    {
        Task<IEnumerable<ReadNursePatientDto>> GetAllNursePatients();
        Task<bool> IsNursePatientExist(int nurse, int patient);
        Task<ReadNursePatientDto?> AssignNurseToPatient(AddNursePatientDto? nursePatientDto);
        Task<ReadNursePatientDto?> UpdateNursePatient(UpdateNursePatientDto nursePatientDto);
        Task<ReadNursePatientDto?> RemoveNursePatient(int nurseId, int patientId);
        Task<IEnumerable<ReadNursePatientDto?>> GetAssignmentsForPatient(int patientId);
    }
}
