using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.Room;

namespace HospitalManagementSystem.Mappers
{
    public class RoomProfile : AutoMapper.Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomReadDto>()
                .ForMember(d => d.DepartmentName,
                    o => o.MapFrom(s => s.Department.DepartmentName))
                .ForMember(d => d.RoomStatusName,
                    o => o.MapFrom(s => s.RoomStatus.Status))
                .ForMember(d => d.AdmissionsCount,
                    o => o.MapFrom(s => s.Admissions.Count));

            CreateMap<RoomCreateDto, Room>();
            CreateMap<RoomUpdateDto, Room>();
        }
    }
}
