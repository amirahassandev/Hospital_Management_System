namespace HospitalManagementSystem.Data.Dto.Doctor
{
    public class DoctorReadDto
    {
        public int DoctorId { get; set; }
        public int UserId { get; set; }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public int SpecializationId { get; set; }
        public string SpecializationName { get; set; } = null!;

        public int? YearsOfExperience { get; set; }
        public string? Certificate { get; set; }

        public bool IsActive { get; set; }
        public decimal ConsultationFee { get; set; }
    }
}
