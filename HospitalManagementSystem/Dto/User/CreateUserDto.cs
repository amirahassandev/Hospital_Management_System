namespace HospitalManagementSystem.Dto.User
{
    public class CreateUserDto
    {
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string Phone { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? RoleName { get; set; } = "Patient";
        //public virtual Doctor? Doctor { get; set; }
        //public virtual Nurse? Nurse { get; set; }
        //public virtual Patient? Patient { get; set; }
        //public virtual Receptionist? Receptionist { get; set; }
    }
}

