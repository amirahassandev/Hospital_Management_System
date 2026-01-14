using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.Doctor;

namespace HospitalManagementSystem.Mappers
{
    public class DoctorProfile : AutoMapper.Profile
    {
        public DoctorProfile()
        {
            CreateMap<Doctor, DoctorReadDto>()
               .ForMember(d => d.FirstName,
                    o => o.MapFrom(s => s.User.FirstName))
                .ForMember(d => d.LastName,
                    o => o.MapFrom(s => s.User.LastName))
               .ForMember(dest => dest.Email,
                   opt => opt.MapFrom(src => src.User.Email))
               .ForMember(dest => dest.SpecializationName,
                   opt => opt.MapFrom(src => src.Specialization.SpecializationName));

            CreateMap<DoctorCreateDto, Doctor>();
            CreateMap<DoctorUpdateDto, Doctor>();
        }
    }
}
