namespace HospitalManagementSystem.Dto.Department
{
    public class DepartmentReadDto
    {
       
            public int DepartmentId { get; set; }
            public string DepartmentName { get; set; } = null!;
            public string? DepartmentDescription { get; set; }

            public int NursesCount { get; set; }
            public int RoomsCount { get; set; }
    }
}

