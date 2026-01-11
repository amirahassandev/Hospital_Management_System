namespace HospitalManagementSystem.Data.Dto.Doctor
{
    public class DoctorCreateDto
    {
        public int UserId { get; set; }
        public int SpecializationId { get; set; }
        public int? YearsOfExperience { get; set; }
        public string? Certificate { get; set; }
        public string LicenseNumber { get; set; } = null!;
        public decimal ConsultationFee { get; set; }
    }
}
