using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.MediaclRecord;
using HospitalManagementSystem.Dto.Prescription;

namespace HospitalManagementSystem.Mappers
{
    public class PrescriptionProfile : AutoMapper.Profile
    {
        public PrescriptionProfile()
        {
            CreateMap<AddPrescriptionDto, Prescription>()
                .ForMember(p => p.MedicalId, o => o.MapFrom(ap => ap.MedicalId));

            //CreateMap<MedicalRecord, MedicalRecordReadDto>()
            //    .ForMember(d => d.PatientFirstName, o => o.MapFrom(s => s.Patient.User.FirstName))
            //    .ForMember(d => d.PatientLastName, o => o.MapFrom(s => s.Patient.User.LastName))
            //    .ForMember(d => d.DoctorFirstName, o => o.MapFrom(s => s.Doctor.User.FirstName))
            //    .ForMember(d => d.DoctorLastName, o => o.MapFrom(s => s.Doctor.User.LastName))
            //    .ForMember(d => d.PrescriptionsCount, o => o.MapFrom(s => s.Prescriptions.Count))
            //    .ForMember(d => d.AppointmentsCount, o => o.MapFrom(s => s.Appointments.Count));

            CreateMap<Prescription, ReadPrescriptionDto>()
                .ForMember(rp => rp.medicalRecordDto, o => o.MapFrom(p => p.Medical));


        }
    }
}
