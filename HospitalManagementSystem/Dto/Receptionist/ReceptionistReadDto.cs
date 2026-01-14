namespace HospitalManagementSystem.Dto.Receptionist
{
    public class ReceptionistReadDto
    {
        public int ReceptionistId { get; set; }

        public DateOnly ReceptionistShift { get; set; }

        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
