using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.Admission;
using HospitalManagementSystem.Dto.Room;

namespace HospitalManagementSystem.Mappers
{
    public class AdmissionProfile:AutoMapper.Profile
    {
        public AdmissionProfile()
        {
            CreateMap<Admission, ReadAdmissionDto>()
                .ForMember(ra => ra.roomReadDto, o => o.MapFrom(a => a.Room));

            CreateMap<AddAdmissionDto, Admission>()
                .ForMember(a => a.Room, o => o.MapFrom(aa => aa.RoomCreateDto));

            CreateMap<UpdateAdmissionDto, Admission>()
                .ForMember(a => a.Patient, o => o.Ignore())
                .ForMember(a => a.Room, o => o.Ignore());


        }
    }
}
