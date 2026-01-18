namespace HospitalManagementSystem.Dto.NursePatient
{
    public class ReadNursePatientDto
    {
        public int NursePatientId { get; set; }
        public int NurseId { get; set; }
        public string NurseName { get; set; } = string.Empty;
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public DateTime? Shift { get; set; }
        public string? Notes { get; set; }
    }
}
