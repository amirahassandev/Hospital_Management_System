using HospitalManagementSystem.Dto.Admission;
using HospitalManagementSystem.Dto.Room;
using HospitalManagementSystem.Dto.User;

namespace HospitalManagementSystem.Dto.Patient
{
    public class ReadPatient
    {
        public int Id { get; set; }
        public string bloodType { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public UserReadDto? userReadDto { get; set; }
        public IEnumerable<ReadAdmissionDto>? readAdmissionDto { get; set; } = new List<ReadAdmissionDto>();
        public IEnumerable<string>? NurseNames { get; set; } = new List<string>();

    }
}
