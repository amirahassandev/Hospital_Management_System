using HospitalManagementSystem.Data.Dto.Nurse;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Data.Mappers
{
    public class NurseProfile : AutoMapper.Profile
    {
        public NurseProfile()
        {
            CreateMap<Nurse, NurseReadDto>()
                .ForMember(d => d.FirstName,
                    o => o.MapFrom(s => s.User.FirstName))
                .ForMember(d => d.LastName,
                    o => o.MapFrom(s => s.User.LastName))
                .ForMember(d => d.Email,
                    o => o.MapFrom(s => s.User.Email))
                .ForMember(d => d.DepartmentName,
                    o => o.MapFrom(s => s.Department.DepartmentName));

            CreateMap<NurseCreateDto, Nurse>();
            CreateMap<NurseUpdateDto, Nurse>();

        }
    }
}
