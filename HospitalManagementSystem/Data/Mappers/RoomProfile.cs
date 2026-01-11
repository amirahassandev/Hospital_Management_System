using HospitalManagementSystem.Data.Dto.Room;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Data.Mappers
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

            CreateMap<Data.Dto.Room.RoomCreateDto, Models.Room>();
            CreateMap<Data.Dto.Room.RoomUpdateDto, Models.Room>();
        }
    }
}
