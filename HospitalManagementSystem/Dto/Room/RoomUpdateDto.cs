namespace HospitalManagementSystem.Dto.Room
{
    public class RoomUpdateDto
    {
        public string RoomNumber { get; set; } = null!;
        public int DepartmentId { get; set; }
        public int RoomStatusId { get; set; }
    }
}
