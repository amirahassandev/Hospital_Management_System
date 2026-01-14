using HospitalManagementSystem.Dto.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagementSystem.Dto.Patient
{
    public class AddPatientDto
    {
        public string BloodType { get; set; } = null!;
        public CreateUserDto createUserDto{ get; set; }
    }
}
