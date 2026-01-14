namespace HospitalManagementSystem.Dto.User
{
    public class UpdateUserDto
    {
        //public string Password { get; set; } = null!;
        //public string Email { get; set; } = null!;
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string? Phone { get; set; } = null!;
        //public DateOnly DateOfBirth { get; set; }
        //public string? Gender { get; set; }
        //public string? RoleName { get; set; }
    }
}
