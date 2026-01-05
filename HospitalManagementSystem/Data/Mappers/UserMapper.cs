using HospitalManagementSystem.Data.Dto.User;
using HospitalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Org.BouncyCastle.Crypto.Generators;
using System.Runtime.CompilerServices;

namespace HospitalManagementSystem.Data.Mappers
{
    public static class UserMapper
    {
        private static readonly HospitalDbContext _db;

        public static UserDto ConvertUserToUserDto(User user)
        {
            if (user == null) { return null; }
            return new UserDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                CreatedAt = user.CreatedAt,
                Role = user.Role == null? null! : user.Role.RoleType,
                Gender = (user.Gender) ? "Male" : "Female",
                DateOfBirth = user.DateOfBirth,
                Age = DateTime.Today.Year - user.DateOfBirth.Year
            };
        }

        public static List<UserDto> ConvertUsersToUsersDto(List<User> users)
        {
            if (users.Count == 0) { return null; }
            List<UserDto> usersDto = new List<UserDto>();
            foreach (var user in users)
            {
                usersDto?.Add(UserMapper.ConvertUserToUserDto(user));
            }
            return usersDto;
        }

        public static User ConvertUserWithPasswordDtoToUserEntity(UserWithPasswordDto user)
        {
            return new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                //PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password),
                PasswordHash = user.Password,
                Age = DateTime.Today.Year - user.DateOfBirth.Year,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                Gender = (user.Gender != null && user.Gender.Trim().ToLower() == "male") ? true : false,
                Phone = user.Phone,
                RoleId = user.RoleId,
                CreatedAt = DateTime.Now,
            };
        }
    }
}
