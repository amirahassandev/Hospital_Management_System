using HospitalManagementSystem.Data.Dto.Receptionist;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Data.Mappers
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
