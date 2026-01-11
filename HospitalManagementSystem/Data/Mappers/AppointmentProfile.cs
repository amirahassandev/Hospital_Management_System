namespace HospitalManagementSystem.Data.Mappers
{
    public class AppointmentProfile : AutoMapper.Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Models.Appointment, Data.Dto.Appointment.AppointmentReadDto>()
                .ForMember(dest => dest.AppointmentStatusName, opt => opt.MapFrom(src => src.AppointmentStatus.Status))
                .ForMember(dest => dest.DoctorFirstName, opt => opt.MapFrom(src => src.Doctor.User.FirstName))
                .ForMember(dest => dest.DoctorLastName, opt => opt.MapFrom(src => src.Doctor.User.LastName))
                .ForMember(dest => dest.PatientFirstName, opt => opt.MapFrom(src => src.Patient.User.FirstName))
                .ForMember(dest => dest.PatientLastName, opt => opt.MapFrom(src => src.Patient.User.LastName))
                .ForMember(dest => dest.Diagnosis, opt => opt.MapFrom(src => src.Medical.Diagnosis));
            CreateMap<Data.Dto.Appointment.AppointmentCreateDto, Models.Appointment>();
            CreateMap<Data.Dto.Appointment.AppointmentUpdateDto, Models.Appointment>();
        }
    }
}
