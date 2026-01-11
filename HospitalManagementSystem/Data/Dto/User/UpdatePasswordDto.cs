namespace HospitalManagementSystem.Data.Dto.User
{
    public class UpdatePasswordDto
    {
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}
