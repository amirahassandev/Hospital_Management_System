using AutoMapper;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.Admission;
using HospitalManagementSystem.Dto.Patient;
using HospitalManagementSystem.Dto.Room;
using HospitalManagementSystem.Dto.User;

namespace HospitalManagementSystem.Mappers
{
    public class PatientProfile: Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, ReadPatient>()
                .ForMember(rp => rp.userReadDto, o => o.MapFrom(p => p.User))
                .ForMember(rp => rp.Id, o => o.MapFrom(p => p.PatientId))
                .ForMember(rp => rp.NurseNames,
                    o => o.MapFrom(p =>
                        p.NursePatients == null
                            ? new List<string>()
                            : p.NursePatients
                                .Where(np => np.Nurse != null && np.Nurse.User != null)
                                .Select(np => np.Nurse.User.FirstName)
                                .ToList()))
                .ForMember(rp => rp.readAdmissionDto, o => o.MapFrom(p => p.Admissions));

            CreateMap<Admission, ReadAdmissionDto>()
                .ForMember(ra => ra.roomReadDto, o => o.MapFrom(a => a.Room));

            CreateMap<Room, RoomReadDto>()
                .ForMember(rr => rr.RoomStatusName, o => o.MapFrom(r => r.RoomStatus.Status))
                .ForMember(rr => rr.DepartmentName, o => o.MapFrom(r => r.Department.DepartmentName));

            CreateMap<AddPatientDto, Patient>()
                .ForMember(p => p.User, o => o.Ignore())
                .ForMember(p => p.UserId, o => o.Ignore());

            CreateMap<User, UserReadDto>();
        }
    }
}
