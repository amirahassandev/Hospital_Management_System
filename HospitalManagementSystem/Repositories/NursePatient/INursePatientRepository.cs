
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.NursePatient;

namespace HospitalManagementSystem.Repositories
{
    public interface INursePatientRepository
    {
        Task<IEnumerable<NursePatient>> GetAllNursePatients();
        Task<bool> IsNursePatientExist(int nurse, int patient);
        Task<NursePatient?> GetNursePatient(int nurseId, int patientId);
        Task<NursePatient?> AssignNurseToPatient(NursePatient nursePatient);
        Task<bool> UpdateNursePatient(NursePatient nursePatient);
        Task<bool> RemoveNursePatient(NursePatient nursePatient);
        //Task<IEnumerable<NursePatient?>> GetAssignmentsForNurse(int nurseId);  // nada
        Task<IEnumerable<NursePatient?>> GetAssignmentsForPatient(int patientId);
        //Task<List<NursePatient?>> GetUpcomingShifts(DateTime fromDate);
        //Task<List<NursePatient?>> SearchAssignments(string keyword);
    }
}
