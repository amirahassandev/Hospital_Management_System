namespace HospitalManagementSystem.Data.Dto.Room
{
    public class RoomReadDto
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; } = null!;

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;

        public int RoomStatusId { get; set; }
        public string RoomStatusName { get; set; } = null!;

        public int AdmissionsCount { get; set; }
    }
}
