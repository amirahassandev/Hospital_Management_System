namespace HospitalManagementSystem.Dto.NursePatient
{
    public class UpdateNursePatientDto
    {
        public int PatientId { get; set; }
        //public string PatientName { get; set; } = string.Empty;
        public int NurseId { get; set; }
        //public string NurseName { get; set; } = string.Empty;
        public DateTime? Shift { get; set; }
        public string? Notes { get; set; }
    }
}
