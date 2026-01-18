namespace HospitalManagementSystem.Dto.ContactMessage
{
    public class ReadMessageDto
    {
        public int MessageId { get; set; }
        public string MessageDescription { get; set; } = null!;
        public int PatientId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string PatientName { get; set; } = null!;
    }
}
