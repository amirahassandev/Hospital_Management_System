using HospitalManagementSystem.Data.Dto.MediaclRecord;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Data.Mappers
{
    public class MedicalRecordProfile : AutoMapper.Profile
    {
        public MedicalRecordProfile()
        {
            CreateMap<MedicalRecord, MedicalRecordReadDto>()
                .ForMember(d => d.PatientFirstName, o => o.MapFrom(s => s.Patient.User.FirstName))
                .ForMember(d => d.PatientLastName, o => o.MapFrom(s => s.Patient.User.LastName))
                .ForMember(d => d.DoctorFirstName, o => o.MapFrom(s => s.Doctor.User.FirstName))
                .ForMember(d => d.DoctorLastName, o => o.MapFrom(s => s.Doctor.User.LastName))
                .ForMember(d => d.PrescriptionsCount, o => o.MapFrom(s => s.Prescriptions.Count))
                .ForMember(d => d.AppointmentsCount, o => o.MapFrom(s => s.Appointments.Count));

            CreateMap<MedicalRecordCreateDto, MedicalRecord>();
            CreateMap<MedicalRecordUpdateDto, MedicalRecord>();
        }
    }
}
