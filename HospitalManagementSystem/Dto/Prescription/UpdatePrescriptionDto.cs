using HospitalManagementSystem.Dto.MediaclRecord;

namespace HospitalManagementSystem.Dto.Prescription
{
    public class UpdatePrescriptionDto
    {
        public string? MedicationName { get; set; }
        public string? Dosage { get; set; } = null!;
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int? MedicalId { get; set; }
    }
}
