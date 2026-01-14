using AutoMapper;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.Department;

namespace HospitalManagementSystem.Mappers
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile() {
            CreateMap<Department, DepartmentReadDto>()
                .ForMember(dest => dest.NursesCount, opt => opt.MapFrom(src => src.Nurses.Count))
                .ForMember(dest => dest.RoomsCount, opt => opt.MapFrom(src => src.Rooms.Count));
            CreateMap<DepartmentCreateDto, Department>();
            CreateMap<DepartmentUpdateDto, Department>();
        }
    }
}
