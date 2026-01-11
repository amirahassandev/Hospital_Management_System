using AutoMapper;

namespace HospitalManagementSystem.Data.Mappers
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile() {
            CreateMap<Models.Department, Data.Dto.Department.DepartmentReadDto>()
                .ForMember(dest => dest.NursesCount, opt => opt.MapFrom(src => src.Nurses.Count))
                .ForMember(dest => dest.RoomsCount, opt => opt.MapFrom(src => src.Rooms.Count));
            CreateMap<Data.Dto.Department.DepartmentCreateDto, Models.Department>();
            CreateMap<Data.Dto.Department.DepartmentUpdateDto, Models.Department>();
        }
    }
}
