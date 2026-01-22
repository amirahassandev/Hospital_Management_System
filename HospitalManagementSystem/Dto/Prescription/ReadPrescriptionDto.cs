using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.MediaclRecord;

namespace HospitalManagementSystem.Dto.Prescription
{
    public class ReadPrescriptionDto
    {
        public int PrescriptionsId { get; set; }
        public string MedicationName { get; set; } = null!;
        public string Dosage { get; set; } = null!;
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public MedicalRecordReadDto medicalRecordDto { get; set; } = null!;
    }
}
