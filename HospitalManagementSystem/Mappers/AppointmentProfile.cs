using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.Appointment;

namespace HospitalManagementSystem.Mappers
{
    public class AppointmentProfile : AutoMapper.Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentReadDto>()
                .ForMember(dest => dest.AppointmentStatusName, opt => opt.MapFrom(src => src.AppointmentStatus.Status))
                .ForMember(dest => dest.DoctorFirstName, opt => opt.MapFrom(src => src.Doctor.User.FirstName))
                .ForMember(dest => dest.DoctorLastName, opt => opt.MapFrom(src => src.Doctor.User.LastName))
                .ForMember(dest => dest.PatientFirstName, opt => opt.MapFrom(src => src.Patient.User.FirstName))
                .ForMember(dest => dest.PatientLastName, opt => opt.MapFrom(src => src.Patient.User.LastName))
                .ForMember(dest => dest.Diagnosis, opt => opt.MapFrom(src => src.Medical.Diagnosis));
            CreateMap<AppointmentCreateDto, Appointment>();
            CreateMap<AppointmentUpdateDto, Appointment>();
        }
    }
}
