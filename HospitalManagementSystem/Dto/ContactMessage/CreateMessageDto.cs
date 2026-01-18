using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagementSystem.Dto.ContactMessage
{
    public class CreateMessageDto
    {
        public string MessageDescription { get; set; } = null!;
        public int PatientId { get; set; }
    }
}
