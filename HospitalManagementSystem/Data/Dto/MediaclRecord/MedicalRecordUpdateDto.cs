namespace HospitalManagementSystem.Data.Dto.MediaclRecord
{
    public class MedicalRecordUpdateDto
    {
        public string Diagnosis { get; set; } = null!;
        public string Treatment { get; set; } = null!;
        public string Notes { get; set; } = null!;
        public DateOnly? RecordDate { get; set; }
    }
}
