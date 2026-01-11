namespace HospitalManagementSystem.Data.Dto.NurseDoctor
{
    public class NurseDoctorReadDto
    {
        public int NurseDoctorId { get; set; }

        public int NurseId { get; set; }
        public string NurseFirstName { get; set; } = null!;
        public string NurseLastName { get; set; } = null!;

        public int DoctorId { get; set; }
        public string DoctorFirstName { get; set; } = null!;
        public string DoctorLastName { get; set; } = null!;

        public DateTime? NurseDoctorShift { get; set; }
        public string? Notes { get; set; }
    }
}
