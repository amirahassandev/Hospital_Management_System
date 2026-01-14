namespace HospitalManagementSystem.Dto.MediaclRecord
{
    public class MedicalRecordReadDto
    {
        public int MedicalId { get; set; }
        public string Diagnosis { get; set; } = null!;
        public string Treatment { get; set; } = null!;
        public string Notes { get; set; } = null!;
        public DateOnly? RecordDate { get; set; }

        public int PatientId { get; set; }
        public string PatientFirstName { get; set; } = null!;
        public string PatientLastName { get; set; } = null!;

        public int DoctorId { get; set; }
        public string DoctorFirstName { get; set; } = null!;
        public string DoctorLastName { get; set; } = null!;

        public int PrescriptionsCount { get; set; }
        public int AppointmentsCount { get; set; }
    }
}
