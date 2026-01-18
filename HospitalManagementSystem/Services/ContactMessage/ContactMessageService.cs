using AutoMapper;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.ContactMessage;
using HospitalManagementSystem.Repositories;

namespace HospitalManagementSystem.Services.ContactMessage
{
    public class ContactMessageService : IContactMessageService
    {
        private readonly IMapper _mapper;
        private readonly IContactMessageRepository _Repository;
        public ContactMessageService(IMapper mapper, IContactMessageRepository Repository) 
        {
            this._mapper = mapper;
            this._Repository = Repository;
        }

        async Task<IEnumerable<ReadMessageDto?>> IContactMessageService.GetAll()
        {
            var messages = await _Repository.GetAll();
            return _mapper.Map<IEnumerable<ReadMessageDto>>(messages);
        }

        async Task<ReadMessageDto?> IContactMessageService.GetMessage(int id)
        {
            var message = await _Repository.GetMessage(id);
            return _mapper.Map<ReadMessageDto>(message);
        }

        async Task<IEnumerable<ReadMessageDto?>> IContactMessageService.GetMessagesOfPatient(int PatientId)
        {
            var messages = await _Repository.GetMessagesOfPatient(PatientId);
            return _mapper.Map<IEnumerable<ReadMessageDto>>(messages);
        }

        async Task<ReadMessageDto?> IContactMessageService.CreateMessage(CreateMessageDto messageDto)
        {
            var message = _mapper.Map<Message>(messageDto);
            if (message == null) return null;
            bool isCreated = await _Repository.CreateMessage(message);
            if (!isCreated) return null;

            return _mapper.Map<ReadMessageDto>(message);
        }

        async Task<ReadMessageDto?> IContactMessageService.DeleteMessage(int messageId)
        {           
            var message = await _Repository.GetMessage(messageId);
            if (message == null) return null;
            bool ifDeleted = await _Repository.DeleteMessage(messageId);
            if(!ifDeleted) return null;
            return _mapper.Map<ReadMessageDto>(message);
        }
        async Task<ReadMessageDto?> IContactMessageService.UpdateMessage(int messageId, UpdateMessageDto messageDto)
        {
            var message = await _Repository.GetMessage(messageId);
            if (message == null) return null;

            _mapper.Map(messageDto, message);

            bool ifUpdated = await _Repository.UpdateMessage(messageId, message);
            if (!ifUpdated) return null;
            return _mapper.Map<ReadMessageDto>(message);
        }


    }
}
