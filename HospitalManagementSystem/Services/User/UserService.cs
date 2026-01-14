using AutoMapper;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models;
using HospitalManagementSystem.Dto.User;
using HospitalManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;


namespace HospitalManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly HospitalDbContext _db;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repo, IMapper mapper, HospitalDbContext db)
        {
            _repo = repo;
            _mapper = mapper;
            _db = db;
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync(bool isActive = true)
        {
            var users = await _repo.GetAllAsync(isActive);
            return _mapper.Map<IEnumerable<UserReadDto>>(users);
        }

        public async Task<UserReadDto?> GetUserByIdAsync(int id)
        {
            var user = await _repo.GetByIdIncludeRoleAsync(id);
            return user == null ? null : _mapper.Map<UserReadDto>(user);
        }

        public async Task<bool> IsActiveAsync(int id)
        {
            var user = await _repo.GetByIdIncludeRoleAsync(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user.IsActive;
        }

        public async Task<UserReadDto> CreateUserAsync(CreateUserDto dto)
        {
            var exists = await _db.Users.AnyAsync(u => u.Email == dto.Email);
            if (exists) throw new Exception("This email is already registered");
            dto.FirstName = dto.FirstName.ToLower().Trim();
            dto.LastName = dto.LastName?.ToLower().Trim();

            var user = _mapper.Map<User>(dto);
            var role = await _db.Roles.FirstOrDefaultAsync(r => r.RoleType == dto.RoleName);
            if (role == null) throw new Exception("Invalid role");

            user.RoleId = role.RoleId;
            user.CreatedAt = DateTime.Now;

            var createdUser = await _repo.AddAsync(user);
            return _mapper.Map<UserReadDto>(createdUser);
        }

        public async Task<UserReadDto?> UpdateUserAsync(int id, UpdateUserDto dto)
        {
            var user = await _repo.GetByIdIncludeRoleAsync(id);
            if (user == null || !user.IsActive) return null;

            bool isUpdated = false;

            if (!string.IsNullOrWhiteSpace(dto.FirstName))
            {
                user.FirstName = dto.FirstName.ToLower().Trim();
                isUpdated = true;
            }

            if (!string.IsNullOrWhiteSpace(dto.LastName))
            {
                user.LastName = dto.LastName.ToLower().Trim();
                isUpdated = true;
            }

            if (!string.IsNullOrWhiteSpace(dto.Phone))
            {
                user.Phone = dto.Phone.Trim();
                isUpdated = true;
            }

            if (!isUpdated) return null;

            var updatedUser = await _repo.UpdateAsync(user);
            return _mapper.Map<UserReadDto>(updatedUser);
        }

        public async Task<UserReadDto?> UpdateEmailAsync(int id, UpdateEmailDto dto)
        {
            var user = await _repo.GetByIdIncludeRoleAsync(id);
            if (user == null || !user.IsActive) return null;

            if (string.IsNullOrWhiteSpace(dto.Email)) return null;

            user.Email = dto.Email;
            var updatedUser = await _repo.UpdateAsync(user);
            return _mapper.Map<UserReadDto>(updatedUser);
        }

        public async Task<UserReadDto?> UpdatePasswordAsync(int id, UpdatePasswordDto dto)
        {
            var user = await _repo.GetByIdIncludeRoleAsync(id);
            if (user == null || !user.IsActive) return null;

            if (string.IsNullOrWhiteSpace(dto.OldPassword) || string.IsNullOrWhiteSpace(dto.NewPassword))
                return null;

            if (dto.OldPassword != user.PasswordHash) return null;

            user.PasswordHash = dto.NewPassword;
            var updatedUser = await _repo.UpdateAsync(user);
            return _mapper.Map<UserReadDto>(updatedUser);
        }

        public async Task<UserReadDto?> DeleteUserAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null || !user.IsActive) return null;

            var deletedUser = await _repo.DeleteAsync(user);
            return _mapper.Map<UserReadDto>(deletedUser);
        }
    }
}
