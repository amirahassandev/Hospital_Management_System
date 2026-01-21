
using HospitalManagementSystem.Dto.Room;

namespace HospitalManagementSystem.Dto.Admission
{
    public class AddAdmissionDto
    {
        public DateOnly? AdmissionDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly? DischargeDate { get; set; }
        public int PatientId { get; set; }
        public RoomCreateDto RoomCreateDto { get; set; } 
    }
}
