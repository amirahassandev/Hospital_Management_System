using AutoMapper;
using HospitalManagementSystem.Data.Dto.NurseDoctor;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Mapping
{
    public class NurseDoctorProfile : Profile
    {
        public NurseDoctorProfile()
        {
            CreateMap<Models.NurseDoctor, NurseDoctorReadDto>()
                .ForMember(d => d.NurseFirstName,
                    o => o.MapFrom(s => s.Nurse.User.FirstName))
                .ForMember(d => d.NurseLastName,
                    o => o.MapFrom(s => s.Nurse.User.LastName))
                .ForMember(d => d.DoctorFirstName,
                    o => o.MapFrom(s => s.Doctor.User.FirstName))
                .ForMember(d => d.DoctorLastName,
                    o => o.MapFrom(s => s.Doctor.User.LastName));

            CreateMap<NurseDoctorCreateDto, Models.NurseDoctor>();
            CreateMap<NurseDoctorUpdateDto, Models.NurseDoctor>();
        }
    }
}

