using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repositories
{
    public class ContactMessageRepository : IContactMessageRepository
    {
        private readonly HospitalDbContext _db;
        private readonly IPationtRepository _patientRepo;
        public ContactMessageRepository(HospitalDbContext db, IPationtRepository patientRepo) 
        {
            this._db = db;
            this._patientRepo = patientRepo;
        }
        async Task<IEnumerable<Message?>> IContactMessageRepository.GetAll()
        {
            return await _db.Messages.Include(c => c.Patient).ThenInclude(p => p.User).ToListAsync();
        }

        async Task<Message?> IContactMessageRepository.GetMessage(int id)
        {
            return await _db.Messages.Include(c => c.Patient).ThenInclude(p => p.User).FirstOrDefaultAsync(c => c.MessageId == id); 
        }

        async Task<IEnumerable<Message?>> IContactMessageRepository.GetMessagesOfPatient(int PatientId)
        {
            return await _db.Messages.Include(c => c.Patient).ThenInclude(p => p.User).Where(c => c.PatientId == PatientId).ToListAsync();
        }

        async Task<bool> IContactMessageRepository.CreateMessage(Message message)
        {
            await _db.Messages.AddAsync(message);
            var isPuted = await _db.SaveChangesAsync();
            return isPuted > 0;
        }

        async Task<bool> IContactMessageRepository.DeleteMessage(int messageId)
        {
            var message = await ((IContactMessageRepository)this).GetMessage(messageId);
            if(message == null) return false;
             _db.Messages.Remove(message);
            var isDeleted = await _db.SaveChangesAsync();
            return isDeleted > 0;
        }

        async Task<bool> IContactMessageRepository.UpdateMessage(int messageId, Message message)
        {
            _db.Messages.Update(message);
            var isUpdated = await _db.SaveChangesAsync();
            return isUpdated > 0;
        }

    }
}
