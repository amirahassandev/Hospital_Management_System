
namespace HospitalManagementSystem.Dto.Admission
{
    public class UpdateAdmissionDto
    {
        public DateOnly? AdmissionDate { get; set; }
        public DateOnly? DischargeDate { get; set; }

        public int PatientId { get; set; }

        public string? RoomNumber { get; set; }

        public int RoomStatusId { get; set; }
    }
}
