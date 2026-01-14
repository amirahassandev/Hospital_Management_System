using AutoMapper;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.Patient;
using HospitalManagementSystem.Dto.User;

namespace HospitalManagementSystem.Mappers
{
    public class PatientProfile: Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, ReadPatient>()
                .ForMember(rp => rp.userReadDto, o => o.MapFrom(p => p.User))
                .ForMember(rp => rp.Id, o => o.MapFrom(p => p.PatientId));

            CreateMap<AddPatientDto, Patient>()
                .ForMember(p => p.User, o => o.Ignore())
                .ForMember(p => p.UserId, o => o.Ignore());

            //CreateMap<ReadPatient, Patient>()
            //    .ForMember(p => p.User, o => o.MapFrom(rp => rp.userReadDto))
            //    .ForMember(p => p.User.Role.RoleType, o => o.MapFrom(rp => rp.userReadDto.Role));

            //CreateMap<CreateUserDto, User>();



            //CreateMap<User, UserReadDto>()
            //    .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.RoleType));


        }
    }
}
