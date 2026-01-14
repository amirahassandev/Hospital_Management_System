namespace HospitalManagementSystem.Dto.Nurse
{
    public class NurseReadDto
    {
        public int NurseId { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
