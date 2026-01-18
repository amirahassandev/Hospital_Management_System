using AutoMapper;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.ContactMessage;

namespace HospitalManagementSystem.Mappers
{
    public class ContactMessageProfile: AutoMapper.Profile
    {
        private string MergeName(string? firstName, string? lastName)
        {
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                return $"{firstName} {lastName}";
            }
            return firstName ?? lastName ?? string.Empty ;
        }
        public ContactMessageProfile() 
        {
            CreateMap<Message, ReadMessageDto>()
                .ForMember(rm => rm.PatientName, o => o.MapFrom(m => MergeName(m.Patient.User.FirstName, m.Patient.User.LastName)));

            CreateMap<CreateMessageDto, Message>()
                .ForMember(m => m.PatientId, o => o.MapFrom(cm => cm.PatientId));

            CreateMap<UpdateMessageDto, Message>();
                //.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
