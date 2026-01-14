using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.Receptionist;

namespace HospitalManagementSystem.Mappers
{
    public class ReceptionistProfile : AutoMapper.Profile
    {
        public ReceptionistProfile()
        {
            CreateMap<Receptionist, ReceptionistReadDto>()
                .ForMember(d => d.FirstName,
                    o => o.MapFrom(s => s.User.FirstName))
                .ForMember(d => d.LastName,
                    o => o.MapFrom(s => s.User.LastName));

            CreateMap<ReceptionistCreateDto, Receptionist>();
            CreateMap<ReceptionistUpdateDto, Receptionist>();
        }
    }
}
