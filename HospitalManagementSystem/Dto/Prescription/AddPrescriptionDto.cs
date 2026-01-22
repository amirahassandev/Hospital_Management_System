using HospitalManagementSystem.Dto.MediaclRecord;

namespace HospitalManagementSystem.Dto.Prescription
{
    public class AddPrescriptionDto
    {
        public string MedicationName { get; set; } = null!;
        public string Dosage { get; set; } = null!;
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int MedicalId { get; set; }
        //public MedicalRecordCreateDto medicalRecordDto { get; set; } = null!;


    }
}
