using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Org.BouncyCastle.Crypto.Generators;
using System.Runtime.CompilerServices;

namespace HospitalManagementSystem.Mappers
{
    public static class UserMapper
    {
        private static readonly HospitalDbContext _db;

        public static UserReadDto ConvertUserToUserDto(User user)
        {
            if (user == null) { return null; }
            return new UserReadDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                CreatedAt = user.CreatedAt,
                Role = user.Role == null? null! : user.Role.RoleType,
                Gender = user.Gender ? "Male" : "Female",
                DateOfBirth = user.DateOfBirth,
                Age = DateTime.Today.Year - user.DateOfBirth.Year
            };
        }

        public static List<UserReadDto> ConvertUsersToUsersDto(List<User> users)
        {
            if (users.Count == 0) { return null; }
            List<UserReadDto> usersDto = new List<UserReadDto>();
            foreach (var user in users)
            {
                usersDto?.Add(ConvertUserToUserDto(user));
            }
            return usersDto;
        }


    }
}
