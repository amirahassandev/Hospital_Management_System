namespace HospitalManagementSystem.Dto.NurseDoctor
{
    public class NurseDoctorCreateDto
    {
        public int NurseId { get; set; }
        public int DoctorId { get; set; }

        public DateTime? NurseDoctorShift { get; set; }
        public string? Notes { get; set; }
    }
}
