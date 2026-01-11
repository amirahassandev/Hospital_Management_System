using HospitalManagementSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagementSystem.Data.Dto.User
{
    public class UserReadDto
    {
        public int UserId { get; set; }
        //public string PasswordHash { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string Phone { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string Role { get; set; } = null!;
        public bool IsActive { get; set; } = true;

        //public virtual Doctor? Doctor { get; set; }
        //public virtual Nurse? Nurse { get; set; }
        //public virtual Patient? Patient { get; set; }
        //public virtual Receptionist? Receptionist { get; set; }
    }
}
