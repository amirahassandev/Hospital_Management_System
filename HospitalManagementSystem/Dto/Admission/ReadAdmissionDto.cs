
using HospitalManagementSystem.Dto.Room;

namespace HospitalManagementSystem.Dto.Admission
{
    public class ReadAdmissionDto
    {
        public int AdmissionsId { get; set; }
        public int PatientId { get; set; }
        public DateOnly? AdmissionDate { get; set; }
        public DateOnly? DischargeDate { get; set; }
        public RoomReadDto? roomReadDto { get; set; }
    }
}
