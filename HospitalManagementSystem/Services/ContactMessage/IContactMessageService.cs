using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.ContactMessage;
using HospitalManagementSystem.Repositories;

namespace HospitalManagementSystem.Services
{
    public interface IContactMessageService
    {
        Task<IEnumerable<ReadMessageDto?>> GetAll();
        Task<ReadMessageDto?> GetMessage(int id);
        Task<IEnumerable<ReadMessageDto?>> GetMessagesOfPatient(int PatientId);
        Task<ReadMessageDto?> CreateMessage(CreateMessageDto messageDto);
        Task<ReadMessageDto?> DeleteMessage(int messageId);
        Task<ReadMessageDto?> UpdateMessage(int messageId, UpdateMessageDto messageDto);
    }
}
