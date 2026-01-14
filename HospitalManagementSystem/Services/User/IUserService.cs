using HospitalManagementSystem.Dto.User;

namespace HospitalManagementSystem.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDto>> GetAllUsersAsync(bool isActive = true);
        Task<UserReadDto?> GetUserByIdAsync(int id);
        Task<bool> IsActiveAsync(int id);
        Task<UserReadDto> CreateUserAsync(CreateUserDto dto);
        Task<UserReadDto?> UpdateUserAsync(int id, UpdateUserDto dto);
        Task<UserReadDto?> UpdateEmailAsync(int id, UpdateEmailDto dto);
        Task<UserReadDto?> UpdatePasswordAsync(int id, UpdatePasswordDto dto);
        Task<UserReadDto?> DeleteUserAsync(int id);
    }
}
