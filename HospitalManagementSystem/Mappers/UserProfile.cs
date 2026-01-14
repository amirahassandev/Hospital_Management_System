using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.User;

namespace HospitalManagementSystem.Mappers
{
    public class UserProfile: AutoMapper.Profile
    {
        private static int CalculateAge(DateOnly dateOfBirth)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            int age = today.Year - dateOfBirth.Year;
            if (today < dateOfBirth.AddYears(age))
                age--;
            return age;
        }
        public UserProfile() 
        {
            CreateMap<User, UserReadDto>()
                .ForMember(ur => ur.Role, o => o.MapFrom(u => u.Role.RoleType))
                .ForMember(ur => ur.Gender, o => o.MapFrom(u => u.Gender ? "Female" : "Male"))
                .ForMember(ur => ur.Age, o => o.MapFrom(u => CalculateAge(u.DateOfBirth)));

            CreateMap<CreateUserDto, User>()
                .ForMember(u => u.Gender, o => o.MapFrom(cr => cr.Gender.ToLower() == "female" ? 1 : 0))
                .ForMember(u => u.PasswordHash, o => o.MapFrom(cr => cr.Password))
                .ForMember(u => u.Role, o => o.Ignore())
                .ForMember(u => u.RoleId, o => o.Ignore());


        }
    }
}
