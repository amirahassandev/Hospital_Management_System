using HospitalManagementSystem.Data.Models;

namespace HospitalManagementSystem.Repositories
{
    public interface IContactMessageRepository
    {
        Task<IEnumerable<Message?>> GetAll();
        Task<Message?> GetMessage(int id);
        Task<IEnumerable<Message?>> GetMessagesOfPatient(int PatientId);
        Task<bool> CreateMessage(Message message);
        Task<bool> DeleteMessage(int messageId);
        Task<bool> UpdateMessage(int messageId, Message message);
    }
}
