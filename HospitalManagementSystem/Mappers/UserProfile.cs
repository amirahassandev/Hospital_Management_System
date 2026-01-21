using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.User;

namespace HospitalManagementSystem.Mappers
{
    public class UserProfile: AutoMapper.Profile
    {
        public UserProfile() 
        {
            CreateMap<User, UserReadDto>()
                .ForMember(ur => ur.Role, o => o.MapFrom(u => u.Role.RoleType))
                .ForMember(ur => ur.Gender, o => o.MapFrom(u => u.Gender ? "Female" : "Male"))
                .ForMember(ur => ur.Age, o => o.MapFrom(u => Helpers.HelperMethods.CalculateAge(u.DateOfBirth)))
                .ForMember(ur => ur.FullName, o => o.MapFrom(u => Helpers.HelperMethods.GetFullName(u.FirstName, u.LastName)));

            CreateMap<CreateUserDto, User>()
                .ForMember(u => u.Gender, o => o.MapFrom(cr => cr.Gender.ToLower() == "female" ? 1 : 0))
                .ForMember(u => u.PasswordHash, o => o.MapFrom(cr => cr.Password))
                .ForMember(u => u.Role, o => o.Ignore())
                .ForMember(u => u.RoleId, o => o.Ignore());


        }
    }
}
