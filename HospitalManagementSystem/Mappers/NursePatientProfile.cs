using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.NursePatient;

namespace HospitalManagementSystem.Mappers
{
    public class NursePatientProfile : AutoMapper.Profile
    {
        
        public NursePatientProfile()
        {
            CreateMap<AddNursePatientDto, NursePatient>()
                .ForMember(np => np.NursePatientShift, o => o.MapFrom(anp => anp.Shift));

            CreateMap<NursePatient, ReadNursePatientDto>()
                .ForMember(rnp => rnp.PatientName, o => o.MapFrom(np => np.Patient.User.FirstName))
                .ForMember(rnp => rnp.NurseName, o => o.MapFrom(np => np.Nurse.User.FirstName))
                .ForMember(rnp => rnp.Shift, o => o.MapFrom(np => np.NursePatientShift));

            //CreateMap<UpdateNursePatientDto, NursePatient>()
            //    .ForMember(np => GetFullName(np.Nurse.User.FirstName, np.Nurse.User.LastName), o => o.MapFrom(unp => unp.NurseName))
            //    .ForMember(np => GetFullName(np.Patient.User.FirstName, np.Patient.User.LastName), o => o.MapFrom(unp => unp.PatientName))
            //    .ForMember(np => np.NursePatientShift, o => o.MapFrom(unp => unp.Shift));

            CreateMap<UpdateNursePatientDto, NursePatient>()
                .ForMember(np => np.NursePatientShift, o => o.MapFrom(unp => unp.Shift));
        }
    }
}
