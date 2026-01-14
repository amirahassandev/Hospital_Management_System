namespace HospitalManagementSystem.Dto.MediaclRecord
{
    public class MedicalRecordCreateDto
    {
        public string Diagnosis { get; set; } = null!;
        public string Treatment { get; set; } = null!;
        public string Notes { get; set; } = null!;
        public DateOnly? RecordDate { get; set; }

        public int PatientId { get; set; }
        public int DoctorId { get; set; }
    }
}
