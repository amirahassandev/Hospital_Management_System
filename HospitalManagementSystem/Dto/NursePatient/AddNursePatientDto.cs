namespace HospitalManagementSystem.Dto.NursePatient
{
    public class AddNursePatientDto
    {
        public int NurseId { get; set; }
        public int PatientId { get; set; }
        public DateTime? Shift { get; set; }
        public string? Notes { get; set; } = null;
    }
}
