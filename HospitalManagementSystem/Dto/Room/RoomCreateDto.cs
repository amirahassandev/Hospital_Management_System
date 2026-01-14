namespace HospitalManagementSystem.Dto.Room
{
    public class RoomCreateDto
    {
        public string RoomNumber { get; set; } = null!;
        public int DepartmentId { get; set; }
        public int RoomStatusId { get; set; }
    }
}
